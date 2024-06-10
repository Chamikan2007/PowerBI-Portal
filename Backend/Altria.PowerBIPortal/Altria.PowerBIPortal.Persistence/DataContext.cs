using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;

namespace Altria.PowerBIPortal.Persistence;

public class DataContext : IdentityDbContext<User, Role, Guid>, IUnitOfWork
{
    public DataContext(DbContextOptions options) : base(options)
    {        
    }

    public DbSet<SubscriptionRequest> SubscriptionRequests { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}