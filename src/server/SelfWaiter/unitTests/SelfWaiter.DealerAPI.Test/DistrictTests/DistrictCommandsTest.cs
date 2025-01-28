using AutoFixture;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using System.Linq.Expressions;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DistrictTests
{
    public class DistrictCommandsTest: BaseTest
    {

        #region CreateDistrictCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateDistrictCommandHandler_WhenDistrictCreated_ReturnsTrue(CreateDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            _districtRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<District, bool>>>()))
                .ReturnsAsync(false);

            _districtRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<District>())).Returns(Task.CompletedTask);

            var handler = new CreateDistrictCommand.CreateDistrictCommandHandler(_districtRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task CreateDistrictCommandHandler_WhenDistrictNameAlreadyExist_ThrowsSelfWaiterException(CreateDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            _districtRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<District, bool>>>()))
                .ReturnsAsync(true);



            var handler = new CreateDistrictCommand.CreateDistrictCommandHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictAlreadyExist, exception.Message);    
        }

        #endregion


        #region CreateRangeDistrictCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeDistrictCommandHandler_WhenDistrictRangeCreate_ReturnsTrue(CreateRangeDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            _districtRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<District, bool>>>()))
                .ReturnsAsync(false);

            _districtRepositoryMock.Setup(x => x.CreateRangeAsync(It.IsAny<IEnumerable<District>>())).Returns(Task.CompletedTask);

            var handler = new CreateRangeDistrictCommand.CreateRangeDistrictCommandHandler(_districtRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task CreateRangeDistrictCommandHandler_WhenAnyDistrictNameAlreadyExist_ThrowsSelfWaiterException(CreateRangeDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            _districtRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<District, bool>>>()))
                .ReturnsAsync(true);

            var handler = new CreateRangeDistrictCommand.CreateRangeDistrictCommandHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictAlreadyExist, exception.Message);
        }


        #endregion


        #region DeleteDistrictCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteDistrictCommandHandler_WhenDistrictDelete_ReturnsTrue(DeleteDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            var returnedDistrictData = Fixture.Create<District>();

            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);

            _districtRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<District>())).Returns(Task.CompletedTask);

            var handler = new DeleteDistrictCommand.DeleteDistrictCommandHandler(_districtRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteDistrictCommandHandler_WhenDistrictNotFound_ThrowsSelfWaiterException(DeleteDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            District? returnedDistrictData = null;

            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);


            var handler = new DeleteDistrictCommand.DeleteDistrictCommandHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictNotFound, exception.Message);
        }

        #endregion

        #region DeleteRangeDistrictCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDistrictCommandHandler_WhenDistrictDeleteRange_ReturnsTrue(DeleteRangeDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            command.Ids = command.Ids.Take(1);

            var returnedDistrictDatas = Fixture.CreateMany<District>(1).AsQueryable();

            _districtRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<District, bool>>>())).Returns(returnedDistrictDatas);

            _districtRepositoryMock.Setup(x => x.DeleteRangeAsync(It.IsAny<IEnumerable<District>>())).Returns(Task.CompletedTask);

            var handler = new DeleteRangeDistrictCommand.DeleteRangeDistrictCommandHandler(_districtRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDistrictCommandHandler_WhenDistrictRangeInConsistency_ThrowsSelfWaiterException(DeleteRangeDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            command.Ids = command.Ids.Take(2);

            var returnedDistrictDatas = Fixture.CreateMany<District>(1).AsQueryable();

            _districtRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<District, bool>>>())).Returns(returnedDistrictDatas);


            var handler = new DeleteRangeDistrictCommand.DeleteRangeDistrictCommandHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);
        }

        #endregion

        #region UpdateDistrictCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task UpdateDistrictCommandHandler_WhenUpdateDistrict_ReturnsTrue(UpdateDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            var returnedDistrictData = Fixture.Create<District>();
            
            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);
            _districtRepositoryMock.Setup(x => x.UpdateAdvance(It.IsAny<District>(), It.IsAny<object>()));

            var handler = new UpdateDistrictCommand.UpdateDistrictCommandHandler(_districtRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task UpdateDistrictCommandHandler_WhenDistrictNotFound_ThrowsSelfWaiterException(UpdateDistrictCommand command)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            District? returnedDistrictData = null;

            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);

            var handler = new UpdateDistrictCommand.UpdateDistrictCommandHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictNotFound, exception.Message);
        }

        #endregion
    }
}
