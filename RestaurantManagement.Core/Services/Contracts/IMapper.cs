using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core.Services.Contracts
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        ICollection<TDestination> Map<TSource, TDestination>(ICollection<TSource> source);
    }
}
