using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MoneyBox.Web.Startup))]

namespace MoneyBox.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(async (context, next) => {
            //    using (container.BeginExecutionContextScope())
            //    {
            //        await next();
            //    }
            //});

            app.Use(async (context, next) => {
                CallContext.LogicalSetData("IOwinContext", context);
                await next();
            });
            ConfigureAuth(app);
        }
    }
}
