using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace DummyMock.Dummier
{
    public class Dummier : IDummier.IDummier
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
