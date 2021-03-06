﻿using Microsoft.EntityFrameworkCore;
using Roots.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IRootsDbContext
    {
        DbSet<Child> Children { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventType> EventTypes { get; set; }
        DbSet<Family> Families { get; set; }
        DbSet<Partner> Partners { get; set; }
        DbSet<PartnerRole> PartnerRoles { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Place> Places { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}