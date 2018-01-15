using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            dummier = new Dummier.DummyFactory();
        }

        public Mock<T> DummyMock<T>() where T : class
        {
            var dynamicMock = new Mock<T>();

            SetupMethodDummies(dynamicMock);

            return dynamicMock;
        }

        public void SetupMethodDummies<T>(Mock<T> dynamicMock) where T : class
        {
            var typeT = typeof(T);
            var tMethods = typeT.GetMethods();
            foreach (var methodInfo in tMethods)
            {
                var lambdaExpression = GetSetup<T>(methodInfo);

                var returnValue = dummier.CreateDummy(methodInfo.ReturnType);
                dynamicMock.Setup(lambdaExpression).Returns(returnValue);
            }
        }

        private Expression<Func<TD, object>> GetSetup<TD>(MethodInfo methodInfo) where TD : class
        {
            var objType = typeof(TD);

            var param = Expression.Parameter(objType, objType.Name);
            var paramss = new[] {param};
            var paramExpressions = methodInfo.GetParameters().Select(GetParamExpression);
            var invoke = Expression.Call(param, methodInfo, paramExpressions);
            var lambda = Expression.Lambda<Func<TD, object>>(invoke, paramss);

            return lambda;
        }

        private Expression GetParamExpression(ParameterInfo parameterInfo)
        {
            var result = Expression.Parameter(parameterInfo.ParameterType, parameterInfo.Name);
            return result;
        }
    }
}
