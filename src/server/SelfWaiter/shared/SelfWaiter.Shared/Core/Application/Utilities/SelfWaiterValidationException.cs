using System.Runtime.Serialization;

namespace SelfWaiter.Shared.Core.Application.Utilities
{
    public class SelfWaiterValidationException : SelfWaiterException
    {
        public IEnumerable<ValidationExceptionModel> Errors { get; }

        public SelfWaiterValidationException():base()
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        public SelfWaiterValidationException(string message) : base(message)
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        public SelfWaiterValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        public SelfWaiterValidationException(string message, Exception inner) : base(message, inner)
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        protected SelfWaiterValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
        {
            IEnumerable<string> arr = errors.Select(
                x => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}"
            );
            return $"Validation failed: {string.Join(string.Empty, arr)}";
        }
    }

    public class ValidationExceptionModel
    {
        public string? Property { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
