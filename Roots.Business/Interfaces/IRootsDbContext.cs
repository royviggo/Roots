using Microsoft.EntityFrameworkCore;
using Roots.Domain.Models;

namespace Roots.Business.Interfaces
{
    public interface IRootsDbContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<EventType> EventTypes { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Place> Places { get; set; }
    }
}