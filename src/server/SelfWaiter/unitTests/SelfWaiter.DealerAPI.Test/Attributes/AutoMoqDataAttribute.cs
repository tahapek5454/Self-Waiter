using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace SelfWaiter.DealerAPI.Test.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
              : base(() =>
              {
                  var _fixture = new Fixture();
                  _fixture.Behaviors
                      .OfType<ThrowingRecursionBehavior>()
                      .ToList()
                      .ForEach(b => _fixture.Behaviors.Remove(b));

                  _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                  return _fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
              })
        {
        }

    }
}
