using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnroladorStandAloneV2.CapaLogicaNegocio
{

    /// <summary>
    /// Clase para las funciones extras del LINQ
    /// </summary>
    public static partial class QueryableExtensions
    {
        /// <summary>
        /// Funcion para Filtrar dinámico por un campo
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="source">Lista Origen</param>
        /// <param name="member">Propiedad</param>
        /// <param name="value">Valor a buscar</param>
        /// <returns></returns>
        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string member, object value)
        {
            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
                value = Convert.ChangeType(value, memberType);
            var condition = Expression.Equal(memberValue, Expression.Constant(value, memberType));
            var predicate = Expression.Lambda<Func<T, bool>>(condition, item);
            return source.Where(predicate);
        }
    }
}
