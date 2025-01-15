namespace SelfWaiter.Shared.Core.Application.Extension
{
    public static class ForeachExtension
    {
        public static void ForeachWithIndex<T>(this IEnumerable<T> source, Action<T, int> handler)
        {
            if (source?.Any() != true || handler == null)
                return;

            int index = 0;
            foreach (var item in source)
                handler.Invoke(item, index++);
        }

        public static void Foreach<T>(this IEnumerable<T> source, Action<T> handler)
        {
            if (source?.Any() != true || handler == null)
                return;

            foreach (var item in source)
                handler.Invoke(item);
        }
    }
}
