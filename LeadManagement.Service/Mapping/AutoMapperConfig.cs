using AutoMapper;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using Microsoft.AspNet.Identity;

namespace LeadManagement.Service.Mapping
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Lead, LeadViewModel>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.ToLowerInvariant()));
            Mapper.CreateMap<IdentityResult, ValidationResultViewModel>()
                .ForMember(d => d.Success, o => o.MapFrom(s => s.Succeeded))
                .ForMember(d => d.Errors, o => o.MapFrom(s => s.Errors));
        }
    }
}
