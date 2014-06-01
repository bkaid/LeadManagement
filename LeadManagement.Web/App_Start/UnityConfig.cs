using System.Data.Entity;
using System.Web.Mvc;
using LeadManagement.Core.Contracts.Mapping;
using LeadManagement.Data;
using LeadManagement.Data.Contracts;
using LeadManagement.Data.Repositories;
using LeadManagement.Model.Domain;
using LeadManagement.Service.Contracts;
using LeadManagement.Service.Mapping;
using LeadManagement.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using Owin;
using Unity.Mvc5;

namespace LeadManagement.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IAppBuilder app)
        {
            var container = new UnityContainer();

            container.RegisterType<IMapper, AutoMapperMapper>();
            container.RegisterType<ILeadRepository, LeadRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ILeadService, LeadService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IUserStore<User>, ApplicationUserStore>();
            container.RegisterInstance<IDataProtector>(app.GetDataProtectionProvider().Create("EmailConfirmation"));
            container.RegisterType<IUserTokenProvider<User, string>, DataProtectorTokenProvider<User>>();
            container.RegisterType<DbContext, ApplicationDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}