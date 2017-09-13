using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Data.Entities
{
    /// <summary>
    /// Event data that is stored in the local database and send to the browser to be shown to the user.
    /// </summary>
    public class CalendarEvent
    {
        [Key]
        public string Id { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// User whom the event belongs to.
        /// </summary>
        public string Owner { get; set; }
    }
}