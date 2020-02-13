﻿using Roots.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Roots.Business.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace Roots.Data.Context
{
    public partial class RootsDbContext : DbContext, IRootsDbContext
    {
        public RootsDbContext(DbContextOptions<RootsDbContext> options) : base(options) { }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Place> Places { get; set; }

        Task IRootsDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}