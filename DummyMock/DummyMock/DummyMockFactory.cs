using System;
using System.Linq.Expressions;
using DummyMock.Dummier.Contracts;
using Moq;

namespace DummyMock
{
    public class DummyMockFactory
    {
        private readonly IDummyFactory dummier;

        public DummyMockFactory(IDummyFactory dummier)
        {
           this.dummier = dummier;
        }

        public DummyMockFactory()
        {
            this.dummier = new Dummier.DummyFactory();
        }

        public Mock<T> DummyMock<T>() where T : class
        {
            var dynamicMock = new Mock<T>();

            var typeT = typeof (T);
            var tMethods = typeT.GetMethods();
            foreach (var methodInfo in tMethods)
            {
                //Class type
                var parameter = Expression.Parameter(typeof(T), methodInfo.Name);

                //String rep of property
                var body = Expression.Call(parameter, methodInfo);

                //build the lambda for the setup method
                var lambdaExpression = Expression.Lambda<Func<T, object>>(body, parameter);

                var returnValue = dummier.CreateDummy(methodInfo.ReturnType);

                dynamicMock.Setup(lambdaExpression).Returns(returnValue);
            }

            return dynamicMock;
        }
    }
}
