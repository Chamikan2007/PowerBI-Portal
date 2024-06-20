using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriptionRequests;

internal class SubscriptionRequestApprovalLevelConfiguration : IEntityTypeConfiguration<SubscriptionRequestApprovalLevel>
{
    public void Configure(EntityTypeBuilder<SubscriptionRequestApprovalLevel> builder)
    {
        builder.Property(e => e.Comment).HasMaxLength(500);
    }
}
