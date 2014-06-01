using System.Threading.Tasks;
using System.Web.Mvc;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using LeadManagement.Service.Contracts;
using LeadManagement.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LeadManagement.Tests.Web.Controllers
{
    [TestClass]
    public class AccountControllerTests : BaseControllerTests
    {
        private AccountController _accountController;
        private Mock<IUserService> _mockUserService;
        private int _emailSentCount;

        [TestInitialize]
        public override void Initialization()
        {
            base.Initialization();
            _emailSentCount = 0;

            _mockUserService = new Mock<IUserService>(MockBehavior.Strict);
            Container.RegisterInstance<IUserService>(_mockUserService.Object);

            _accountController = GetController<AccountController>();
        }

        [TestMethod]
        public void ForgotPasswordRequestForValidAccountSendsEmail()
        {
            _mockUserService.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns((string name) => Task.FromResult(new User()));
            _mockUserService.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<string>())).Returns((string userId) => Task.FromResult(string.Empty));
            _mockUserService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns((string userId, string subject, string message) =>
            {
                _emailSentCount++;
                return Task.FromResult(0);
            });
            
            var viewModel = new ForgotPasswordViewModel { Email = "test@example.com" };
            var result = _accountController.ForgotPassword(viewModel).Result;

            Assert.AreEqual(1, _emailSentCount, "Forgot password should email user.");
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }
    }
}
