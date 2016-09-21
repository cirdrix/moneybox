using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Microsoft.Owin;

namespace MoneyBox.Web.Providers
{
    public interface IOwinContextProvider
    {
        IOwinContext CurrentContext { get; }
    }

    public class CallContextOwinContextProvider : IOwinContextProvider
    {
        public IOwinContext CurrentContext
        {
            get { return (IOwinContext)CallContext.LogicalGetData("IOwinContext"); }
        }
    }
}