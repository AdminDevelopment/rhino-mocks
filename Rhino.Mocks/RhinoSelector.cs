﻿using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Rhino.Mocks.Interceptors;
using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    [Serializable]
    internal class RhinoSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (method.DeclaringType == typeof(IMockInstance))
                return interceptors
                    .Where(x => (x is MockInterceptor))
                    .ToArray();

            if (method.DeclaringType == typeof(IMockExpectationContainer))
                return interceptors
                    .Where(x => (x is MockInterceptor))
                    .ToArray();

            if (method.DeclaringType == typeof(object))
                return interceptors
                    .Where(x => (x is ObjectInterceptor))
                    .ToArray();

            return interceptors
                .Where(x => (x is ProxyInterceptor))
                .ToArray();
        }
    }
}
