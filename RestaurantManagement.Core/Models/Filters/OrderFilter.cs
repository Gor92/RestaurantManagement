using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.Core.Extensions;

namespace RestaurantManagement.Core.Models.Filters
{
    public class OrderFilter : IFilterBase<Order>
    {
        public int TableId { get; set; }
        public int? RestaurantId { get; set; }

        public Expression<Func<Order, bool>> Predicate()
        {
            Expression<Func<Order, bool>> expression = x => true;

            if (TableId > 0)
                expression = expression.And(x => x.TableId == TableId);
            if (RestaurantId.HasValue && RestaurantId > 0)
                expression = expression.And(x => x.RestaurantId == RestaurantId.Value);

            return expression;
        }
    }
}
