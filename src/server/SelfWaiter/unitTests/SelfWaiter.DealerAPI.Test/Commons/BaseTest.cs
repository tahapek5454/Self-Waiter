using AutoFixture;

namespace SelfWaiter.DealerAPI.Test.Commons
{
    public abstract class BaseTest
    {
        protected Fixture Fixture { get; private set; }
        public BaseTest()
        {
            Fixture = new Fixture();
            Fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => Fixture.Behaviors.Remove(b));

            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
