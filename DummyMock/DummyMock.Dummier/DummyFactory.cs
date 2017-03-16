using System;
using DummyMock.Dummier.Contracts;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace DummyMock.Dummier
{
    public class DummyFactory : IDummyFactory
    {
        public object CreateDummy(Type type)
        {
            // Fixture setup
            var fixture = new Fixture();

            var singleMethod = typeof(SpecimenFactory).GetMethod("Create", new[] { typeof(ISpecimenBuilder) });
            singleMethod = singleMethod.MakeGenericMethod(type);
            var resultado = singleMethod.Invoke(fixture, new object[] { fixture });

            return resultado;
        }
    }
}
