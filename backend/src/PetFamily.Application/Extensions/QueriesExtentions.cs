using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PetFamily.Application.Extensions
{
    public static class QueriesExtentions
    {
        public static async Task<PagedList<T>> ToPagedList<T>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var totalCount = await source.CountAsync(cancellationToken);
            var items = await source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var result = new PagedList<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return result;
        }
        public static IQueryable<T> WhereIf<T>(
                this IQueryable<T> source,
                bool condition,
                Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

    }
}
