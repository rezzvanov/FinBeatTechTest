using System.Linq.Expressions;

namespace SimpleDataApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> SelectPage<T>(
            this IQueryable<T> query,
            int? pageSize,
            int? pageNumber)
        {
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                int skipPages = Math.Max(pageNumber.Value - 1, 0);
                int skipCount = skipPages * pageSize.Value;

                query = query.Skip(skipCount);
            }

            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return query;
        }

        public static IQueryable<T> AddFilterIfSet<T>(
            this IQueryable<T> query,
            bool ifSet,
            Expression<Func<T, bool>> filter)
        {
            return ifSet ? query.Where(filter) : query;
        }
    }
}
