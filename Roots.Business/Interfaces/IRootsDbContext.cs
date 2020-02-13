using Microsoft.EntityFrameworkCore;
using Roots.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IRootsDbContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<EventType> EventTypes { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Place> Places { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}