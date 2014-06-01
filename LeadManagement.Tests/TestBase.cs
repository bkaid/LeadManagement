using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadManagement.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        public TestContext TestContext { get; set; }

        public UnityContainer Container { get; set; }

        [TestInitialize]
        public virtual void Initialization()
        {
            Container = new UnityContainer();
        }
    }

    public static class UnityExtensions
    {
        public static T Resolve<T>(this UnityContainer container)
        {
            return (T)container.Resolve(typeof(T));
        }
        
        public static void RegisterInstance<T>(this UnityContainer container, object instance)
        {
            container.RegisterInstance(typeof(T), instance);
        }
    }
}
