using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations.Interceptors;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Extensions;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext
{
    public class AppDbContext: DbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppDbContext> _logger;
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<UserProfileAndDealer> UserProfileAndDealers { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, ILogger<AppDbContext> logger) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public override int SaveChanges()
        {
            var entites = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in entites)
            {
                switch (entity.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        {
                            entity.Entity.UpdatedDate =  entity.Entity.UpdatedDate is null ||  default(DateTime) == entity.Entity.UpdatedDate ? DateTime.Now : entity.Entity.UpdatedDate;
                            entity.Entity.UpdatetorUserName = GetCurrentUserName();
                        }
                        break;
                    case EntityState.Added:
                        {
                            entity.Entity.CreatedDate = default(DateTime) == entity.Entity.CreatedDate ? DateTime.Now : entity.Entity.CreatedDate;
                            entity.Entity.CreatorUserName = GetCurrentUserName();
                        }
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entites = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in entites)
            {
                switch (entity.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        {
                            entity.Entity.UpdatedDate = entity.Entity.UpdatedDate is null || default(DateTime) == entity.Entity.UpdatedDate ? DateTime.Now : entity.Entity.UpdatedDate;
                            entity.Entity.UpdatetorUserName = GetCurrentUserName();
                        }
                        break;
                    case EntityState.Added:
                        {
                            entity.Entity.CreatedDate = default(DateTime) == entity.Entity.CreatedDate ? DateTime.Now : entity.Entity.CreatedDate;
                            entity.Entity.CreatorUserName = GetCurrentUserName();
                        }
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.AddGlobalQueryFilterForIsValid();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.AddInterceptors(new QueryLoggerInterceptor(_logger));

            base.OnConfiguring(optionsBuilder);
        }

        private string GetCurrentUserName()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? (user?.Identity?.Name ?? "System") : "System";
        }


    }
}
