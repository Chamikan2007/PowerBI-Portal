using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionApprovals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriptionRequests;

internal class SubscriptionApprovalStepConfiguration : IEntityTypeConfiguration<SubscriptionApprovalStep>
{
    public void Configure(EntityTypeBuilder<SubscriptionApprovalStep> builder)
    {
        builder.Property(e => e.Comment).HasMaxLength(500);
    }
}
