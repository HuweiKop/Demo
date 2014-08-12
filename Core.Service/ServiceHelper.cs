using Castle.DynamicProxy;
using Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class ServiceHelper
    {
        public static ServiceFactory serviceFactory = new RefServiceFactory();

        public static T CreateService<T>() where T : class
        {
            var service = serviceFactory.CreateService<T>();

            ProxyGenerator generator = new ProxyGenerator();
            T dynamicProxy = generator.CreateInterfaceProxyWithTargetInterface<T>(service, new InvokeInterceptor());

            return dynamicProxy;
        }
    }

    internal class InvokeInterceptor : IInterceptor
    {
        public InvokeInterceptor()
        { }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                var message = new
                {
                    method = Convert.ToString(invocation.Method),
                    arguments = invocation.Arguments,
                    returnValue = invocation.ReturnValue
                };

                Log4NetHelper.Error(message, ex);
                throw;
            }
        }
    }
}
