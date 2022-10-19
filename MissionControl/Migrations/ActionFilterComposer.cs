/*using MissionControl.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace MissionControl.Migrations
{
    public class ActionFilterComposer : UmbracoApplication, IComposer
    {
        public void Compose(Composition composition)
        {
            // want to register custom exception filter here
            GlobalConfiguration.Configuration.Filters.Add(new BasicAuthenticationAttribute("MissionControl", "asdf123456"));
        }
    }
}
/**/