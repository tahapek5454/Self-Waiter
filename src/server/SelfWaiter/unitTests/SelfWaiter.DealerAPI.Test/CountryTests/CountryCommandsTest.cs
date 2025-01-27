using AutoFixture;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using System.Linq.Expressions;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CountryTests
{
    public class CountryCommandsTest: BaseTest
    {

        #region CreateCountryCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateCountryCommandHandler_WhenCreateCountry_ReturnsTrue(CreateCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            _countryRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(false);

            _countryRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Country>()))
                .Returns(Task.CompletedTask);
                

            var handler = new CreateCountryCommand.CreateCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateCountryCommandHandler_WhenCountryNameAlreadyExist_ThrowsSelfWaiterException(CreateCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            _countryRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(true);

            var handler = new CreateCountryCommand.CreateCountryCommandHandler(_countryRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.CountryAlreadyExist, exception.Message);
        }

        #endregion

        #region CreateRangeCountryCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCountryCommandHandler_WhenCreateCountries_ReturnsTrue(CreateRangeCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            _countryRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(false);

            _countryRepositoryMock
                .Setup(repo => repo.CreateRangeAsync(It.IsAny<IEnumerable<Country>>()))
                .Returns(Task.CompletedTask);


            var handler = new CreateRangeCountryCommand.CreateRangeCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCountryCommandHandler_WhenAnyCountryNameAlredyExist_ThrowsSelfWaiterException(CreateRangeCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            _countryRepositoryMock
                .Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(true);


            var handler = new CreateRangeCountryCommand.CreateRangeCountryCommandHandler(_countryRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.CountryAlreadyExist, exception.Message);
        }


        #endregion

        #region DeleteCountryCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteCountryCommandHandler_WhenDeleteCountry_ReturnsTrue(DeleteCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            var returnedCountryData = Fixture.Create<Country>();
            _countryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(returnedCountryData);

            _countryRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<Country>()))
                .Returns(Task.CompletedTask);


            var handler = new DeleteCountryCommand.DeleteCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteCountryCommandHandler_WhenCountryNotFound_ReturnsFalse(DeleteCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            Country? returnedCountryData = null;
            _countryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(returnedCountryData);

            var handler = new DeleteCountryCommand.DeleteCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response);
        }

        #endregion

        #region DeleteRangeCountryCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCountryCommandHandler_WhenDeleteCountries_ReturnsTrue(DeleteRangeCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            command.Ids = command.Ids.Take(1);
            var returnedCountryDatas = Fixture
                                        .CreateMany<Country>()
                                        .Take(1)
                                        .AsQueryable();

            _countryRepositoryMock.Setup(x  => x.Where(It.IsAny<Expression<Func<Country, bool>>>())).Returns(returnedCountryDatas);

            _countryRepositoryMock.Setup(x => x.DeleteRangeAsync(It.IsAny<IEnumerable<Country>>())).Returns(Task.CompletedTask);


            var handler = new DeleteRangeCountryCommand.DeleteRangeCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCountryCommandHandler_WhenBecomeInconsistency_ThrowsSelfWaiterException(DeleteRangeCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            command.Ids = command.Ids.Take(1);
            var returnedCountryDatas = Fixture
                                        .CreateMany<Country>(3)
                                        .AsQueryable();

            _countryRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<Country, bool>>>())).Returns(returnedCountryDatas);

            var handler = new DeleteRangeCountryCommand.DeleteRangeCountryCommandHandler(_countryRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);
        }

        #endregion

        #region UpdateCountryCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task UpdateCountryCommandHandler_WhenUpdateCountry_ReturnsTrue(UpdateCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            var returnedCountryData = Fixture.Create<Country>();
            _countryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(returnedCountryData);

            _countryRepositoryMock
                .Setup(repo => repo.UpdateAdvance(It.IsAny<Country>(), It.IsAny<object>()));


            var handler = new UpdateCountryCommand.UpdateCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task UpdateCountryCommandHandler_WhenTargetCountryNotFound_ReturnsFalse(UpdateCountryCommand command)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            Country? returnedCountryData = null;
            _countryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(returnedCountryData);

            var handler = new UpdateCountryCommand.UpdateCountryCommandHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response);
        }

        #endregion
    }
}
