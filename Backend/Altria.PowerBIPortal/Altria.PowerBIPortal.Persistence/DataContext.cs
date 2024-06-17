using Altria.PowerBIPortal.Domain;
using Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Altria.PowerBIPortal.Persistence;

public class DataContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
{
    private readonly RequestContext _requestContext;

    public DataContext(DbContextOptions options, RequestContext? requestContext = default) : base(options)
    {
        IsInDesignTime = requestContext == default;
        _requestContext = requestContext == default ? new RequestContext { DisplayName = "", Email = "" } : requestContext;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Rename identity tables

        builder.Entity<User>().ToTable("Users");
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<UserClaim>().ToTable("UserClaims");
        builder.Entity<UserRole>().ToTable("UserRoles");
        builder.Entity<UserLogin>().ToTable("UserLogins");
        builder.Entity<RoleClaim>().ToTable("RoleClaims");
        builder.Entity<UserToken>().ToTable("UserTokens");

        #endregion

        #region Generate ids on add

        var entities = builder.Model.GetEntityTypes()
            .Where(e => typeof(Entity).IsAssignableFrom(e.ClrType));

        foreach (var entity in entities)
        {
            var entry = builder.Entity(entity.ClrType);

            entry.HasKey("Id");
            entry.Property<Guid>("Id").ValueGeneratedOnAdd();
        }

        #endregion

        #region Set delete behaviour of foreign keys

        if (IsInDesignTime)
        {
            builder.Model.GetEntityTypes()
                .Where(et => !et.IsOwned())
                .SelectMany(t => t.GetForeignKeys())
                .ToList()
                .ForEach(fk => { fk.DeleteBehavior = DeleteBehavior.NoAction; });
        }

        #endregion

        #region Set default length for strings

        builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(t => t.ClrType == typeof(string))
            .ToList()
            .ForEach(t =>
            {
                t.SetMaxLength(50);
            });

        #endregion

        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public bool IsInDesignTime { get; }

    public virtual DbSet<Subscription> Subscription { get; set; } = default!;

    public virtual DbSet<ApprovalOfficer> ApprovalOfficer { get; set; } = default!;

    public virtual DbSet<SubscriptionWhiteListEntry> SubscriptionWhiteListEntry { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries<Entity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList()
                .ForEach(entry =>
                {
                    entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = _requestContext.UserId;

                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _requestContext.UserId;
                    }
                });

        return base.SaveChangesAsync(cancellationToken);
    }
}