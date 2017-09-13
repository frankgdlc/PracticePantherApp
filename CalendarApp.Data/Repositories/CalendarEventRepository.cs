using CalendarApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarApp.Data.Repositories
{
    /// <summary>
    /// Implementation of the <see cref="GenericRepository{TEntity, TContext}"/> for <see cref="CalendarEvent"/>.
    /// </summary>
    public class CalendarEventRepository : GenericRepository<CalendarEvent, CalendarContext>
    {
        public CalendarEventRepository(CalendarContext context) : base(context)
        {
        }

        /// <summary>
        /// Fetches the calendar events of a given user.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>A collection with the specified user's calendar events.</returns>
        public IEnumerable<CalendarEvent> GetByOwner(string owner)
        {
            return DbSet.Where(e => e.Owner == owner);
        }

        /// <summary>
        /// Inserts the passed event into the database, only if it has not been already added (to avoid duplicates).
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <returns></returns>
        public int AddIfNew(CalendarEvent calendarEvent)
        {
            var existing = GetById(calendarEvent.Id);
            if (existing == null)
            {
                DbSet.Add(calendarEvent);
                return DbContext.SaveChanges();
            }

            return 0; // Keeping consistency with the standard of returning the amount of records inserted.
        }

        /// <summary>
        /// Asynchronous version of <see cref="AddIfNew(CalendarEvent)"/>.
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <returns></returns>
        public async Task<int> AddIfNewAsync(CalendarEvent calendarEvent)
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
