using CalendarApp.Data.Entities;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarApp.Web.Helpers
{
    public class GoogleCalendarHelper
    {
        /// <summary>
        /// Queries the logged user's Google Calendar and fetches all the events for the next 30 days.
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public static IEnumerable<Event> FetchEvents(UserCredential credential)
        {
            // Create Google Calendar API service.
            var calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Practice Panther Calendar Sync",
            });

            // Define parameters of request.
            var request = calendarService.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.TimeMax = DateTime.Today.AddDays(30);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            var googleEvents = request.Execute();

            return googleEvents != null
                ? googleEvents.Items
                : Enumerable.Empty<Event>();
        }

        /// <summary>
        /// Creates a simple version of the Google Calendar event with only the basic information to store in the local database and to show in the grid.
        /// </summary>
        /// <param name="source">Google Calendar event instance</param>
        /// <param name="owner">User</param>
        /// <returns>Local event instance</returns>
        public static CalendarEvent Convert(Event source, string owner)
        {
            var result = new CalendarEvent
            {
                Owner = owner,
                Id = source.Id,
                Summary = source.Summary,
                Description = source.Description
            };

            DateTime startDate;
            if (DateTime.TryParse(source.Start.Date, out startDate))
                result.StartDate = startDate;

            DateTime endDate;
            if (DateTime.TryParse(source.End.Date, out endDate))
                result.EndDate = endDate;

            return result;
        }
    }
}