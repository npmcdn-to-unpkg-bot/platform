namespace Allors.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Allors.Domain;
    using Allors.Meta;
    using Allors.Web.Workspace;

    public class ExecuteResponseBuilder
    {
        private readonly ISession session;
        private readonly ExecuteRequest executeRequest;
        private string @group;
        private User user;

        public ExecuteResponseBuilder(ISession session, User user, ExecuteRequest executeRequest, string @group)
        {
            this.session = session;
            this.user = user;
            this.executeRequest = executeRequest;
            this.group = group;
        }

        public ExecuteResponse Build()
        {
            if (this.executeRequest.M != null && this.executeRequest.I != null && this.executeRequest.V != null)
            {
                var obj = this.session.Instantiate(this.executeRequest.I);
                var composite = (Composite)obj.Strategy.Class;
                var methodTypes = composite.MethodTypesByGroup[@group];
                var methodType = methodTypes.FirstOrDefault(x => x.Name.Equals(this.executeRequest.M));

                if (methodType != null)
                {
                    var acl = new AccessControlList(obj, this.user);
                    if (acl.CanExecute(methodType))
                    {
                        var method = obj.GetType().GetMethod(methodType.Name, new Type[] { });
                        method.Invoke(obj, null);

                        var derivationLog = this.session.Derive();
                        if (!derivationLog.HasErrors)
                        {
                            this.session.Commit();
                            return new ExecuteResponse { HasErrors = false };
                        }
                    }
                }
            }

            return new ExecuteResponse { HasErrors = true };
        }
    }
}