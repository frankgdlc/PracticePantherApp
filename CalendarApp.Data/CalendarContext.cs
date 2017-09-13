using CalendarApp.Data.Entities;
using System.Data.Entity;

namespace CalendarApp.Data
{
    public class CalendarContext : DbContext
    {
        public CalendarContext() : base("DefaultConnection") { }

        DbSet<CalendarEvent> CalendarEvents { get; set; }
    }
}