using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Extensions
{
    public static class QueryFilterExtension
    {
        public static ModelBuilder AddGlobalQueryFilterForIsValid(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(entityType.ClrType))
                {
                    dynamic entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
                    var parameter = Expression.Parameter(entityType.ClrType, "x");
                    var isValidProperty = Expression.Property(parameter, "IsValid");
                    var lambda = Expression.Lambda(isValidProperty, parameter);
                    entityTypeBuilder.HasQueryFilter(lambda);
                }
            }

            return modelBuilder;
        }
    }
}
