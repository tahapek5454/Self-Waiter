using AutoFixture;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using System.Linq.Expressions;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CityTests
{
    public class CityCommandsTest: BaseTest
    {

        #region CreateCityCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateCityCommandHandler_WhenCreateCity_ReturnsTrue(CreateCityCommand command) 
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            _cityRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<City, bool>>>()))
                .ReturnsAsync(false);

            _cityRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<City>())).Returns(Task.CompletedTask);

            var handler = new CreateCityCommand.CreateCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateCityCommandHandler_WhenCityNameAlreadyExist_ThrowsSelfWaiterException(CreateCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            _cityRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<City, bool>>>()))
                .ReturnsAsync(true);

            var handler = new CreateCityCommand.CreateCityCommandHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAnyAsync<SelfWaiterException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.CityAlreadyExist, exception.Message);
        }

        #endregion


        #region CreateRangeCityCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCityCommandHandler_WhenCreateRangeCity_ReturnsTrue(CreateRangeCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            _cityRepositoryMock
                        .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<City, bool>>>()))
                        .ReturnsAsync(false);

            _cityRepositoryMock.Setup(repo => repo.CreateRangeAsync(It.IsAny<IEnumerable<City>>())).Returns(Task.CompletedTask);

            var handler = new CreateRangeCityCommand.CreateRangeCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCityCommandHandler_WhenRangeCityNamesAlreadyExist_ThrowsSelfWaiterException(CreateRangeCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            _cityRepositoryMock
                        .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<City, bool>>>()))
                        .ReturnsAsync(true);


            var handler = new CreateRangeCityCommand.CreateRangeCityCommandHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async() =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.CityAlreadyExist, exception.Message);
        }

        #endregion

        #region DeleteCityCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteCityCommandHandler_WhenDeleteCity_ReturnsTrue(DeleteCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            var returnedCityData = Fixture.Create<City>();
            _cityRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);

            _cityRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<City>())).Returns(Task.CompletedTask);

            var handler = new DeleteCityCommand.DeleteCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);

        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteCityCommandHandler_WhenCityNotFound_ReturnsFalse(DeleteCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            City? returnedCityData = null;
            _cityRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);


            var handler = new DeleteCityCommand.DeleteCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result);

        }

        #endregion

        #region DeleteRangeCityCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCityCommandHandler_WhenDeleteRangeCity_ReturnsTrue(DeleteRangeCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            command.Ids = command.Ids.Take(1);
            var returnedCitiesDatas = Fixture.CreateMany<City>().Take(1).AsQueryable();
            _cityRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<City, bool>>>())).Returns(returnedCitiesDatas);

            _cityRepositoryMock.Setup(x => x.DeleteRangeAsync(It.IsAny<IEnumerable<City>>())).Returns(Task.CompletedTask);

            var handler = new DeleteRangeCityCommand.DeleteRangeCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);

        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCityCommandHandler_WhenRealiseInConsistency_ThrowsSelfWaiterException(DeleteRangeCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            command.Ids = command.Ids.Take(1);
            var returnedCitiesDatas = Fixture.CreateMany<City>().Take(2).AsQueryable();
            _cityRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<City, bool>>>())).Returns(returnedCitiesDatas);

            var handler = new DeleteRangeCityCommand.DeleteRangeCityCommandHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);

        }

        #endregion

        #region UpdateCityCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task UpdateCityCommandHandler_WhenUpdateCity_ReturnsTrue(UpdateCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            var returnedCityData = Fixture.Create<City>();
            _cityRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);

            _cityRepositoryMock.Setup(x => x.UpdateAdvance(It.IsAny<City>(), It.IsAny<object>()));

            var handler = new UpdateCityCommand.UpdateCityCommandHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }



        [AutoMoqData]
        [Theory]
        public async Task UpdateCityCommandHandler_WhenCityNotFound_ThrowsSelfWaiterException(UpdateCityCommand command)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            City? returnedCityData = null;
            _cityRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);


            var handler = new UpdateCityCommand.UpdateCityCommandHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.CityNotFound, exception.Message);
        }


        #endregion
    }
}
