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

            Assert.IsNotNull(mock.Object.MetodoString());
            Assert.IsNotNull(mock.Object.MetodoString().Name);
            Assert.AreEqual(mock.Object.MetodoString(), mock.Object.MetodoString());
            Assert.AreEqual(mock.Object.MetodoString().Name, mock.Object.MetodoString().Name);
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
        ValueClass MetodoString();
    }

    public class ValueClass
    {
        public virtual string Name { get; set; }
    }
}
