using CalendarApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Data.Repositories
{
    public class CalendarEventRepository : GenericRepository<CalendarEvent, CalendarContext>
    {
        public CalendarEventRepository(CalendarContext context) : base(context)
        {
        }

        public IEnumerable<CalendarEvent> GetByOwner(string owner)
        {
            return DbSet.Where(e => e.Owner == owner);
        }

        public int AddIfNew(CalendarEvent calendarEvent)
        {
            var existing = GetById(calendarEvent.Id);
            if (existing == null)
            {
                DbSet.Add(calendarEvent);
                return DbContext.SaveChanges();
            }
            return 0;
        }

        public async Task<int> InsertIfNewAsync(CalendarEvent calendarEvent)
        {
            var existing = GetByIdAsync(calendarEvent.Id);
            if (existing == null)
            {
                DbSet.Add(calendarEvent);
                return await DbContext.SaveChangesAsync();
            }
            return 0;
        }
    }
}
