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
        public async Task<ActionResult> Index(CancellationToken cancellationToken, [FromUri] bool dataOnly = false)
        {
            var flowMetadata = new AuthorizationCodeMvcApp(this, new CalendarAppFlowMetadata());
            var result = await flowMetadata.AuthorizeAsync(cancellationToken);
            if (!dataOnly)
            {
                var model = new HomeIndexViewModel { RedirectUri = result?.RedirectUri };
                return View(model);
            }
            else
            {
                if (result.Credential == null)
                    return new EmptyResult();

                var googleEvents = GoogleCalendarHelper.FetchEvents(result.Credential);
                var calendarEvents = new List<CalendarEvent>();

                using (var context = new CalendarContext())
                using (var repository = new CalendarEventRepository(context))
                {
                    foreach (var entry in googleEvents)
                    {
                        var calendarEvent = GoogleCalendarHelper.Convert(entry, User.Identity.Name);
                        repository.AddIfNew(calendarEvent);
                        calendarEvents.Add(calendarEvent);
                    }
                }
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