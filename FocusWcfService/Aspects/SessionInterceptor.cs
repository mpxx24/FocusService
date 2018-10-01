using Castle.DynamicProxy;
using FocusWcfService.Common;

namespace FocusWcfService.Aspects {
    public class SessionInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            NHibernateHelper.OpenSession();

            invocation.Proceed();

            NHibernateHelper.CloseSession();
        }
    }
}