using AutoMapper;
using LeadManagement.Core.Contracts.Mapping;

namespace LeadManagement.Service.Mapping
{
    public class AutoMapperMapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
