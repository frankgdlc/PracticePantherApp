using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Data.Entities
{
    public class CalendarEvent
    {
        [Key]
        public string Id { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Owner { get; set; }
    }
}