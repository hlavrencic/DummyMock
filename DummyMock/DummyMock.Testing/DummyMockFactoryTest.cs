using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DummyMock.Testing
{
    [TestClass]
    public class DummyMockFactoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var factory = new DummyMockFactory(new Dummier.DummyFactory());
            var mock = factory.DummyMock<ITestInterface>();

            Assert.AreEqual(mock.Object.MetodoString(), mock.Object.MetodoString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            var factory = new Dummier.DummyFactory();
            var dummy = factory.CreateDummy(typeof(ValueClass)) as ValueClass;

            Assert.IsNotNull(dummy);
        }
    }

    public interface ITestInterface
    {
        string MetodoString();
    }

    public class ValueClass
    {
        public virtual string Name { get; set; }
    }
}
