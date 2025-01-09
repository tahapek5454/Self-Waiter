namespace SelfWaiter.Shared.Core.Application.Utilities
{

	[Serializable]
	public class SelfWaiterException : Exception
	{
		public SelfWaiterException() { }
		public SelfWaiterException(string message) : base(message) { }
		public SelfWaiterException(string message, Exception inner) : base(message, inner) { }
		protected SelfWaiterException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
