using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestInvoice.Repository;
using TestInvoice.Services;

namespace TestInvoice.Ioc
{
    public class Concrete :DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public Concrete()
        {
            ninjectKernel = new StandardKernel();
            AddBinding();
        }

        void AddBinding()
        {
            ninjectKernel.Bind<IMangeInvoice>().To<MangeInvoice>();

        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}