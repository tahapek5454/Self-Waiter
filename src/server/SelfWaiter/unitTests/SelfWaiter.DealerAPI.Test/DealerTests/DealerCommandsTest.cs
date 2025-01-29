using AutoFixture;
using MockQueryable;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using System.Linq.Expressions;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DealerTests
{
    public class DealerCommandsTest: BaseTest
    {

        #region AddUsersToDealerCommandHandler


        [AutoMoqData]
        [Theory]
        public async Task AddUsersToDealerCommandHandler_WhenAddUserToDealer_ReturnsTrue(AddUsersToDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var _userProfileAndDealerRepositoryMock = new Mock<IUserProfileAndDealerRepository>();

            var returnedDealerData = Fixture.CreateMany<Dealer>();
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, arrayIds[index++]).CreateMany(arrayIds.Count());


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());

            var handler = new AddUsersToDealerCommand.AddUsersToDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, _userProfileAndDealerRepositoryMock.Object, MediatorBasic);
            var result = await handler.Handle(command, CancellationToken.None);


            Assert.True(result);

        }


        [AutoMoqData]
        [Theory]
        public async Task AddUsersToDealerCommandHandler_WhenAddUserToDealerIsInActive_ReturnsTrue(AddUsersToDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var _userProfileAndDealerRepositoryMock = new Mock<IUserProfileAndDealerRepository>();

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var userProfileAndDealersEx = Fixture.Build<UserProfileAndDealer>()
                                                 .With(x => x.UserProfileId, () =>
                                                 {
                                                     return arrayIds[index++];
                                                 })
                                                 .CreateMany(arrayIds.Count())
                                                 .ToList();

            var returnedDealerData = Fixture.Build<Dealer>()
                                    .With(x => x.UserProfileAndDealers, userProfileAndDealersEx)
                                    .CreateMany(1);
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            index = 0;
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, arrayIds[index++]).CreateMany(arrayIds.Count());


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());

            var handler = new AddUsersToDealerCommand.AddUsersToDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, _userProfileAndDealerRepositoryMock.Object, MediatorBasic);
            var result = await handler.Handle(command, CancellationToken.None);


            Assert.True(result);

        }


        [AutoMoqData]
        [Theory]
        public async Task AddUsersToDealerCommandHandler_WhenDealerNotFound_ThrowsSelfWaiterException(AddUsersToDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var _userProfileAndDealerRepositoryMock = new Mock<IUserProfileAndDealerRepository>();

            var returnedDealerData = Fixture.CreateMany<Dealer>();
            

            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());

            var handler = new AddUsersToDealerCommand.AddUsersToDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, _userProfileAndDealerRepositoryMock.Object, MediatorBasic);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None)); 


            Assert.Equal(ExceptionMessages.DealerNotFound, exception.Message);

        }


        [AutoMoqData]
        [Theory]
        public async Task AddUsersToDealerCommandHandler_WhenUsersAndRegisteredUserInConsistency_ThrowsSelfWaiterException(AddUsersToDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var _userProfileAndDealerRepositoryMock = new Mock<IUserProfileAndDealerRepository>();

            var returnedDealerData = Fixture.CreateMany<Dealer>();
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, arrayIds[index++]).CreateMany(1);


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());

            var handler = new AddUsersToDealerCommand.AddUsersToDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, _userProfileAndDealerRepositoryMock.Object, MediatorBasic);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));


            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);

        }


        #endregion


        #region CreateDealerCommandHandler

        [AutoMoqData]
        [Theory]
        public async Task CreateDealerCommandHandler_WhenCreateDealer_ReturnsTrue(CreateDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            _dealerRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Dealer, bool>>>())).ReturnsAsync(false);

            var handler = new CreateDealerCommand.CreateDealerCommandHandler(_dealerRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task CreateDealerCommandHandler_WhenDealerNameAlreadyExist_ThrowsSelfWaiterException(CreateDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            _dealerRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Dealer, bool>>>())).ReturnsAsync(true);

            var handler = new CreateDealerCommand.CreateDealerCommandHandler(_dealerRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async()=> await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DealerAlreadyExist, exception.Message);
        }


        #endregion

        #region DeleteDealerCommandHandler


        [AutoMoqData]
        [Theory]
        public async Task DeleteDealerCommandHandler_WhenDeleteDealer_ReturnsTrue(DeleteDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            var returnedDealerData = Fixture.Create<Dealer>();
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);

            var handler = new DeleteDealerCommand.DeleteDealerCommandHandler(_dealerRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteDealerCommandHandler_WhenDealerNotFound_ThrowsSelfWaiterException(DeleteDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            Dealer? returnedDealerData = null;
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);

            var handler = new DeleteDealerCommand.DeleteDealerCommandHandler(_dealerRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DealerNotFound, exception.Message);
        }


        #endregion


        #region DeleteRangeDealerCommandHandler


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDealerCommandHandler_WhenDeleteDealerRange_ReturnsTrue(DeleteRangeDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            var returnedDealersData = Fixture.CreateMany<Dealer>(command.Ids.Count()).AsQueryable();

            _dealerRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<Dealer, bool>>>())).Returns(returnedDealersData);

            var handler = new DeleteRangeDealerCommand.DeleteRangeDealerCommandHandler(_dealerRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDealerCommandHandler_WhenDealerCountInConsistency_ThrowsSelfWaiterException(DeleteRangeDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            var returnedDealersData = Fixture.CreateMany<Dealer>(command.Ids.Count()+1).AsQueryable();

            _dealerRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<Dealer, bool>>>())).Returns(returnedDealersData);

            var handler = new DeleteRangeDealerCommand.DeleteRangeDealerCommandHandler(_dealerRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);
        }

        #endregion


        #region RemoveUsersFromDealerCommandHandler


        [AutoMoqData]
        [Theory]
        public async Task RemoveUsersFromDealerCommandHandler_WhenRemoveUserFromDealer_ReturnsTrue(RemoveUsersFromDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var userProfileAndDealersEx = Fixture.Build<UserProfileAndDealer>()
                                                 .With(x => x.UserProfileId, () =>
                                                 {
                                                     return arrayIds[index++];
                                                 })
                                                 .CreateMany(arrayIds.Count())
                                                 .ToList();

            var returnedDealerData = Fixture.Build<Dealer>()
                                    .With(x => x.UserProfileAndDealers, userProfileAndDealersEx)
                                    .CreateMany(1);
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            index = 0;
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, ()=> arrayIds[index++]).CreateMany(arrayIds.Count());


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());

            var handler = new RemoveUsersFromDealerCommand.RemoveUsersFromDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, MediatorBasic);
            var result = await handler.Handle(command, CancellationToken.None);


            Assert.True(result);

        }


        [AutoMoqData]
        [Theory]
        public async Task RemoveUsersFromDealerCommandHandler_WhenDealerNotFound_ThrowSelfWaiterExceptions(RemoveUsersFromDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();

            var returnedDealerData = Fixture.Build<Dealer>().CreateMany(1);
            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
           

            var handler = new RemoveUsersFromDealerCommand.RemoveUsersFromDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, MediatorBasic);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DealerNotFound, exception.Message);

        }


        [AutoMoqData]
        [Theory]
        public async Task RemoveUsersFromDealerCommandHandler_WhenUserCountCreateInConsistency_ThrowSelfWaiterExceptions(RemoveUsersFromDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var userProfileAndDealersEx = Fixture.Build<UserProfileAndDealer>()
                                                 .With(x => x.UserProfileId, () =>
                                                 {
                                                     return arrayIds[index++];
                                                 })
                                                 .CreateMany(arrayIds.Count())
                                                 .ToList();

            var returnedDealerData = Fixture.Build<Dealer>()
                                    .With(x => x.UserProfileAndDealers, userProfileAndDealersEx)
                                    .CreateMany(1);
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            index = 0;
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, () => arrayIds[index++]).CreateMany(arrayIds.Count() -1);


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());


            var handler = new RemoveUsersFromDealerCommand.RemoveUsersFromDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, MediatorBasic);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);

        }


        [AutoMoqData]
        [Theory]
        public async Task RemoveUsersFromDealerCommandHandler_WhenNotFoundBindedUser_ThrowSelfWaiterExceptions(RemoveUsersFromDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _userProfileRepositoryMock = new Mock<IUserProfileRepository>();



            var returnedDealerData = Fixture.Build<Dealer>()
                                    .CreateMany(1);
            returnedDealerData.First().Id = command.DealerId;
            returnedDealerData.First().IsValid = true;

            int index = 0;
            var arrayIds = command.UserIds.ToArray();
            var returnedUserIdDatas = Fixture.Build<UserProfile>().With(x => x.Id, () => arrayIds[index++]).CreateMany(arrayIds.Count());


            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerData.AsQueryable().BuildMock());
            _userProfileRepositoryMock.Setup(x => x.Query()).Returns(returnedUserIdDatas.AsQueryable().BuildMock());


            var handler = new RemoveUsersFromDealerCommand.RemoveUsersFromDealerCommandHandler(_dealerRepositoryMock.Object, _userProfileRepositoryMock.Object, MediatorBasic);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.InconsistencyExceptionMessage, exception.Message);

        }

        #endregion


        #region UpdateDealerCommandHandler


        [AutoMoqData]
        [Theory]
        public async Task UpdateDealerCommandHandler_WhenUpdateDealer_ReturnsTrue(UpdateDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            var returnedDealerData = Fixture.Create<Dealer>();
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);

            var handler = new UpdateDealerCommand.UpdateDealerCommandHandler(_dealerRepositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task UpdateDealerCommandHandler_WhenDealerNotFound_ThrowsSelfWaiterException(UpdateDealerCommand command)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            Dealer? returnedDealerData = null;
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);

            var handler = new UpdateDealerCommand.UpdateDealerCommandHandler(_dealerRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async ()=> await handler.Handle(command, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DealerNotFound, exception.Message);
        }

        #endregion
    }
}
