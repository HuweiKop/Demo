using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 分页数据集合，用于后端返回分页好的集合及前端视图分页控件绑定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            CurrentPageIndex = pageIndex;
            for (int i = StartRecordIndex - 1; i < EndRecordIndex; i++)
            {
                Add(items[i]);
            }
        }

        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(items);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
            PrePage = CurrentPageIndex - 1 >= 1 ? CurrentPageIndex - 1 : 1;
            NextPage = CurrentPageIndex + 1 <= TotalPageCount ? CurrentPageIndex + 1 : TotalPageCount;
        }

        public int PrePage { get; set; }
        public int NextPage { get; set; }
        public int ExtraCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount
        {
            get
            {
                int result = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
                result = result <= 0 ? 1 : result;
                return result;
            }
        }
        public int StartRecordIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }
        public int EndRecordIndex { get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; } }
    }

    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            
            var totalItemCount = allItems.Count();
            int totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            totalPageCount = totalPageCount <= 0 ? 1 : totalPageCount;
            if (pageIndex < 1)
                pageIndex = 1;
            else if (pageIndex > totalPageCount)
                pageIndex = totalPageCount;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize).ToList();
            
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }
    }

    public static class OrderLinqExtensions
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
string orderAs) where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(orderByProperty)) { 
                orderByProperty = "Id";
            }
            string command = "desc".Equals(orderAs) ? "OrderByDescending" : "OrderBy";

            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
