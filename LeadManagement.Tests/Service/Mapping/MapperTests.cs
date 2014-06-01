using System.Linq;
using LeadManagement.Core.Contracts.Mapping;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using LeadManagement.Service.Mapping;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadManagement.Tests.Service.Mapping
{
    [TestClass]
    public class MapperTests : TestBase
    {
        private IMapper _mapper;
        
        [TestInitialize]
        public override void Initialization()
        {
            base.Initialization();
            Container.RegisterType<IMapper, AutoMapperMapper>();
            AutoMapperConfig.RegisterMappings();
            _mapper = Container.Resolve<IMapper>();
        }

        [TestMethod]
        public void CanMapLeadToLeadViewModel()
        {
            var lead = new Lead { FirstName = "first", LastName = "last" };
            
            var viewModel = _mapper.Map<Lead, LeadViewModel>(lead);

            Assert.IsNotNull(viewModel);
            Assert.AreEqual(lead.FirstName, viewModel.FirstName);
            Assert.AreEqual(lead.LastName, viewModel.LastName);
        }

        [TestMethod]
        public void CanMapIdentityResultToValidationResultViewModel()
        {
            var identityResult = new IdentityResult("error1", "error2");

            var viewModel = _mapper.Map<IdentityResult, ValidationResultViewModel>(identityResult);

            Assert.IsNotNull(viewModel);
            Assert.AreEqual(2, viewModel.Errors.Count);
            Assert.IsTrue(viewModel.Errors.Contains(identityResult.Errors.First()));
        }
    }
}
