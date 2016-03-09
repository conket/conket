using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Ivs.Core.Common
{
    public static class QueryExtention
    {
        public static SelectList ToSelectList<T>(this IQueryable<T> query, string dataValueField, string dataTextField, object selectedValue)
        {
            return new SelectList(query, dataValueField, dataTextField, selectedValue ?? -1);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction)
        {
            string methodName = string.Format("OrderBy{0}",
              direction.ToLower() == "asc" ? "" : "descending");
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            MemberExpression memberAccess = null;
            //
            foreach (var property in sortColumn.Split('.'))
            {
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
            }
            //
            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);

            MethodCallExpression result = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { query.ElementType, memberAccess.Type },
                        query.Expression,
                        Expression.Quote(orderByLambda));
            return query.Provider.CreateQuery<T>(result);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction, bool processOnSouce)
        {
            if (processOnSouce)
            {
                IQueryable<T> result = query;
                if (direction.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    result = from enumerable in query
                             orderby GetPropertyValue(enumerable, sortColumn)
                             select enumerable;
                }
                else
                {

                    result = from enumerable in query
                             orderby GetPropertyValue(enumerable, sortColumn) descending
                             select enumerable;
                }
                return result;
            }
            else
            {
                return OrderBy<T>(query, sortColumn, direction);
            }            
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}