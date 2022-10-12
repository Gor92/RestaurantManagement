using AutoMapper;
using RestaurantManagement.Core.Entities;
using RestaurantManagement.API.ViewModels;
using RestaurantManagement.Core.Services.Contracts;
using IMapper = RestaurantManagement.Core.Services.Contracts.IMapper;

namespace RestaurantManagement.API
{
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper _mapper;
        public Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderReadonlyViewModel, Order>();
                cfg.CreateMap<OrderDetailsReadonlyViewModel, OrderDetails>();

                cfg.CreateMap<Restaurant, RestaurantReadonlyViewModel>();
                cfg.CreateMap<Table, TableReadonlyViewModel>();



                //cfg.CreateMap<A, B>().ConvertUsing<MyConvertor>();
            });
            _mapper = config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);

        }

        public ICollection<TDestination> Map<TSource, TDestination>(ICollection<TSource> source)
        {
            return source.Select(Map<TSource, TDestination>).ToList();
        }
    }
}
