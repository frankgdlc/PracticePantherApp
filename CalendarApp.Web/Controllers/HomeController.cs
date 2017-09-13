using CalendarApp.Data;
using CalendarApp.Data.Entities;
using CalendarApp.Data.Repositories;
using CalendarApp.Web.Helpers;
using CalendarApp.Web.Models;
using CalendarApp.Web.OAuth;
using Google.Apis.Auth.OAuth2.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace CalendarApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            // Authorize the user using the mechanism provided by the API library.
            var flowMetadata = new AuthorizationCodeMvcApp(this, new CalendarAppFlowMetadata());
            var result = await flowMetadata.AuthorizeAsync(cancellationToken);

            if (!HttpContext.Request.IsAjaxRequest()) // The call was made directly to the browser
            {
                // No need to redirect after the user has already authenticated with Google.
                var model = new HomeIndexViewModel { RedirectUri = result?.RedirectUri };
                return View(model); // Render the Razor view and output HTML in the response.
            }
            else // The call was made from the grid.
            {
                if (result.Credential == null)
                    return new EmptyResult();

                // Bring the events from the user's Google Calendar.
                var googleEvents = GoogleCalendarHelper.FetchEvents(result.Credential);
                var calendarEvents = new List<CalendarEvent>();

                using (var context = new CalendarContext())
                using (var repository = new CalendarEventRepository(context))
                {
                    foreach (var entry in googleEvents)
                    {
                        var calendarEvent = GoogleCalendarHelper.Convert(entry, User.Identity.Name); // Create a local version of each event.
                        await repository.AddIfNewAsync(calendarEvent); // Store it in the database if it's new.
                        calendarEvents.Add(calendarEvent);
                    }
                }
                // Send the events' data back to the client to be shown in the grid.
                return Json(calendarEvents, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Practice Panther Calendar App";

            return View();
        }
    }
}