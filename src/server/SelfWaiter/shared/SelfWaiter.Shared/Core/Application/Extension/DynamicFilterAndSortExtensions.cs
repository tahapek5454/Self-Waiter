using System.Linq.Dynamic.Core;
using System.Text;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.Shared.Core.Application.Extension
{
    public static class DynamicFilterAndSortExtensions //MSSQL
    {
        private static readonly string[] _orders = { "asc", "desc" };
        private static readonly string[] _logics = { "and", "or" };

        private static readonly IDictionary<string, string> _operators = new Dictionary<string, string>
    {
        { "eq", "=" },
        { "neq", "!=" },
        { "lt", "<" },
        { "lte", "<=" },
        { "gt", ">" },
        { "gte", ">=" },
        { "isnull", "== null" },
        { "isnotnull", "!= null" },
        { "startswith", "StartsWith" },
        { "endswith", "EndsWith" },
        { "contains", "Contains" },
        { "doesnotcontain", "Contains" }
    };

        public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicRequest dynamicRequest)
        {
            if (dynamicRequest.Filter is not null)
                query = Filter(query, dynamicRequest.Filter);
            if (dynamicRequest.Sort is not null && dynamicRequest.Sort.Any())
                query = Sort(query, dynamicRequest.Sort);
            return query;
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
        {
            IList<Filter> filters = GetAllFilters(filter);
            string?[] values = filters.Select(f => f.Value).ToArray();
            string where = Transform(filter, filters);
            if (!string.IsNullOrEmpty(where) && values != null)
                queryable = queryable.Where(where, values);

            return queryable;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            foreach (Sort item in sort)
            {
                if (string.IsNullOrEmpty(item.Field))
                    throw new ArgumentException("Invalid Field - Dynamic Sort Field Cannot be Null or Empty");
                if (string.IsNullOrEmpty(item.Dir) || !_orders.Contains(item.Dir))
                    throw new ArgumentException("Invalid Order Type - Dynamic Sort Dir Cannot be Null or Empty");
            }

            if (sort.Any())
            {
                string ordering = string.Join(separator: ",", values: sort.Select(s => $"{s.Field} {s.Dir}"));
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }

        public static IList<Filter> GetAllFilters(Filter filter)
        {
            List<Filter> filters = new();
            GetFilters(filter, filters);
            return filters;
        }

        private static void GetFilters(Filter filter, IList<Filter> filters)
        {
            filters.Add(filter);
            if (filter.Filters is not null && filter.Filters.Any())
                foreach (Filter item in filter.Filters)
                    GetFilters(item, filters);
        }

        public static string Transform(Filter filter, IList<Filter> filters)
        {
            if (string.IsNullOrEmpty(filter.Field))
                throw new ArgumentException("Invalid Field - Dynamic Filter Field Cannot be Null or Empty");
            if (string.IsNullOrEmpty(filter.Operator) || !_operators.ContainsKey(filter.Operator))
                throw new ArgumentException("Invalid Operator - Dynamic Filter Operator Cannot be Null or Empty");

            int index = filters.IndexOf(filter);
            string comparison = _operators[filter.Operator];
            StringBuilder where = new();

            if (!string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Operator == "doesnotcontain")
                    where.Append($"(!np({filter.Field}).{comparison}(@{index.ToString()}))");
                else if (comparison is "StartsWith" or "EndsWith" or "Contains")
                    where.Append($"(np({filter.Field}).{comparison}(@{index.ToString()}))");
                else
                    where.Append($"np({filter.Field}) {comparison} @{index.ToString()}");
            }
            else if (filter.Operator is "isnull" or "isnotnull")
            {
                where.Append($"np({filter.Field}) {comparison}");
            }

            if (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
            {
                if (!_logics.Contains(filter.Logic))
                    throw new ArgumentException("Invalid Logic - Dynamic Filter");
                return $"{where} {filter.Logic} ({string.Join(separator: $" {filter.Logic} ", value: filter.Filters.Select(f => Transform(f, filters)).ToArray())})";
            }

            return where.ToString();
        }
    }
}
