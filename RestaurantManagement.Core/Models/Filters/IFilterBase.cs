using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Models.Filters
{
    internal interface IFilterBase<T> where T : class
    {
        Expression<Func<T, bool>> Predicate();
    }
}
