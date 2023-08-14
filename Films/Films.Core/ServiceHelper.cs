using Kirel.Repositories.Core.Models;
using Kirel.Shared;

namespace Films.Core;

/// <summary>
/// 
/// </summary>
public static class ServiceHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderBy"></param>
    /// <param name="orderDirection"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? GenerateOrderingMethod<TEntity>(string? orderBy, SortDirection orderDirection)
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderingMethod = null;
        if (string.IsNullOrEmpty(orderBy)) return orderingMethod;
        var orderExpression = PredicateBuilder.ToLambda<TEntity>(orderBy);
        if (orderExpression == null) return orderingMethod;
        switch (orderDirection)
        {
            case SortDirection.Asc:
                orderingMethod = o => o.OrderBy(orderExpression);
                break;
            case SortDirection.Desc:
                orderingMethod = o => o.OrderByDescending(orderExpression);
                break;
        }
        return orderingMethod;
    }
}