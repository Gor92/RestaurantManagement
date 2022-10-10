using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.Extensions
{
    /// <summary>
    /// Parameter Rebinder 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// Initializes a new instance of <see cref="ParameterRebinder"/>
        /// </summary>
        /// <param name="map">map</param>
        private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) => _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        /// <summary>
        /// Replaces expression parameters
        /// </summary>
        /// <param name="map">Dictionary map</param>
        /// <param name="exp">Expression</param>
        /// <returns>Expression node</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <summary>
        /// Expression Visit Parameter
        /// </summary>
        /// <param name="p">Parameter expression to visit</param>
        /// <returns>Expression node</returns>
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out var replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
