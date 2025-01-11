namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public class Filter(string field, string @operator)
    {
        public string Field { get; set; } = field;
        public string? Value { get; set; }
        public string Operator { get; set; } = @operator;
        public string? Logic { get; set; }

        public IEnumerable<Filter>? Filters { get; set; }

        public Filter():this(string.Empty, string.Empty)
        {
            
        }
    }
}
