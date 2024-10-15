using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Terminal.Backend.Application.Common.QueryParameters;
using Terminal.Backend.Application.Exceptions;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagingParameters parameters)
        => queryable.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize);

    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source,
        OrderingParameters parameters)
    {
        if (parameters.Direction is null)
        {
            parameters = parameters with { Direction = OrderDirection.Ascending };
        }

        var command = parameters.Direction switch
        {
            OrderDirection.Ascending => "OrderBy",
            OrderDirection.Descending => "OrderByDescending",
            _ => throw new UnreachableException()
        };

        var type = typeof(TEntity);
        var parameter = Expression.Parameter(type, "p");

        var properties = parameters.OrderBy.Split('.');

        Expression propertyAccess = parameter;
        foreach (var property in properties)
        {
            var propertyInfo = type.GetProperty(property, BindingFlags.IgnoreCase |  BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo is null)
            {
                throw new ColumnNotFoundException(property);
            }

            propertyAccess = Expression.MakeMemberAccess(propertyAccess, propertyInfo);
            type = propertyInfo.PropertyType;
        }

        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, [typeof(TEntity), type],
            source.Expression, Expression.Quote(orderByExpression));

        return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
    }
}
