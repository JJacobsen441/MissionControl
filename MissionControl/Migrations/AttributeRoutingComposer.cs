using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace MissionControl.Migrations
{
    public class AttributeRoutingComponent : IComponent
    {
        public void Initialize()
        {
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

        }

        public void Terminate()
        {

        }
    }

    public class AttributeRoutingComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<AttributeRoutingComponent>(); ;
        }
    }/**/
}
