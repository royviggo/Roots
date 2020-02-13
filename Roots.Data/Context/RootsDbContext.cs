using Roots.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Roots.Business.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Roots.Domain.Common;

namespace Roots.Data.Context
{
    public partial class RootsDbContext : DbContext, IRootsDbContext
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public RootsDbContext(DbContextOptions<RootsDbContext> options, ICurrentUser currentUser, IDateTime dateTime) : base(options)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Place> Places { get; set; }

        Task<int> IRootsDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.Now;
                        entry.Entity.ModifiedDate = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}