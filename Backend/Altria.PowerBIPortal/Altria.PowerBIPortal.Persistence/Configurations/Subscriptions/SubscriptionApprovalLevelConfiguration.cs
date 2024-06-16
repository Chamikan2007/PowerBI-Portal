using Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.Subscriptions;

internal class SubscriptionApprovalLevelConfiguration : IEntityTypeConfiguration<SubscriptionApprovalLevel>
{
    public void Configure(EntityTypeBuilder<SubscriptionApprovalLevel> builder)
    {
        builder.Property(e => e.Comment).HasMaxLength(500);
    }
}
