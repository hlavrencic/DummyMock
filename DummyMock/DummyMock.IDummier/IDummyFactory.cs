using System;

namespace DummyMock.Dummier.Contracts
{
    public interface IDummyFactory
    {
        object CreateDummy(Type type);
    }
}
