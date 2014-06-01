namespace LeadManagement.Core.Contracts.Mapping
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
