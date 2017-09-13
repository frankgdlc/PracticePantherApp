using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Auth.OAuth2.Flows;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;

namespace CalendarApp.Web.OAuth
{
    public class CalendarAppFlowMetadata : FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow _flow;

        static CalendarAppFlowMetadata()
        {
            _flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "947385166904-bu6qe86d48kem7qc3gsfi8rol7t5u6su.apps.googleusercontent.com",
                    ClientSecret = "n_ZTlxHHi69ZKjA9jE404-y4"
                },
                Scopes = new[] { CalendarService.Scope.CalendarReadonly }, // Only needs read access to the user's calendar.
                DataStore = new LocalDbDataStore() // Use custom DataStore implementation.
            });
        }

        public override IAuthorizationCodeFlow Flow => _flow;

        public override string GetUserId(Controller controller)
        {
            // Use the user's email as the key to the DataStore to keep record of each user's authorization and refresh token obtained from Google.
            return controller.User.Identity.Name;
        }
    }
}