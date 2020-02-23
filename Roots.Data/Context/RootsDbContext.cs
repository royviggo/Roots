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
        private readonly IDateTimeService _dateTime;

        public RootsDbContext(DbContextOptions<RootsDbContext> options, IDateTimeService dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PartnerRole> PartnerRoles { get; set; }
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