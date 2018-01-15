using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: InternalsVisibleTo("DummyMock.Testing")]

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

            Assert.IsNotNull(mock.Object.Method1(string.Empty));
            Assert.IsNotNull(mock.Object.Method1(string.Empty).Name);
            Assert.AreEqual(mock.Object.Method1(string.Empty), mock.Object.Method1(string.Empty));
            Assert.AreEqual(mock.Object.Method1(string.Empty).Name, mock.Object.Method1(string.Empty).Name);

            Assert.AreEqual(3, mock.Object.Method2().Count);
            Assert.IsNotNull(mock.Object.Method2()[0].Name);
            Assert.AreEqual(mock.Object.Method2()[0], mock.Object.Method2()[0]);
            Assert.AreEqual(mock.Object.Method2()[0].Name, mock.Object.Method2()[0].Name);

            Assert.AreNotEqual(mock.Object.Method1("B"), mock.Object.Method1("A"));
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
        ValueClass Method1(string param1);

        IList<ValueClass> Method2();
    }

    public class ValueClass
    {
        public virtual string Name { get; set; }
    }
}
