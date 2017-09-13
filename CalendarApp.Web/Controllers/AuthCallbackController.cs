using Google.Apis.Auth.OAuth2.Mvc;
using CalendarApp.Web.OAuth;

namespace CalendarApp.Web.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData => new CalendarAppFlowMetadata();
    }
}