using Google.Apis.Auth.OAuth2.Mvc;
using CalendarApp.Web.OAuth;

namespace CalendarApp.Web.Controllers
{
    /// <summary>
    /// Implementation of Google OAuth 2 client library for .NET. Specifies the FlowMetadata implementation.
    /// </summary>
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData => new CalendarAppFlowMetadata();
    }
}