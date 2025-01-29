using AutoFixture;
using MediatR;
using Moq;

namespace SelfWaiter.DealerAPI.Test.Commons
{
    public abstract class BaseTest
    {
        protected Fixture Fixture { get; private set; }

        private Mock<IMediator> _mediatorMock;
        protected IMediator MediatorBasic {  get=> _mediatorMock.Object;  }
        public BaseTest()
        {
            Fixture = new Fixture();
            Fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => Fixture.Behaviors.Remove(b));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

             object? o = new();
            _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>())).ReturnsAsync(o);
            _mediatorMock.Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        }
    }
}
