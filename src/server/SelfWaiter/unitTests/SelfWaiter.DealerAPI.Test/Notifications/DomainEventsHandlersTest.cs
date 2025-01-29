using Microsoft.Extensions.Caching.Hybrid;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEvents;
using SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEventsHandlers;
using SelfWaiter.DealerAPI.Test.Commons;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.Notifications
{
    public class DomainEventsHandlersTest:  BaseTest
    {
        [Fact]
        public void DealerChangedEventHandler_WhenDealerChanged_ReturnTask()
        {

            DealerChangedEvent @event = new DealerChangedEvent()
            {
                Tags = new List<string> { "test"}
            };

            var _hybridCacheMock = new Mock<HybridCache>();

            _hybridCacheMock.Setup(x => x.RemoveByTagAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>())).Returns(ValueTask.CompletedTask);

            var handler = new DealerChangedEventHandler(_hybridCacheMock.Object);
            var result =  handler.Handle(@event, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
