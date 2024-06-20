using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriptionRequests;

internal class SubscriptionRequestConfiguration : IEntityTypeConfiguration<SubscriptionRequest>
{
    public void Configure(EntityTypeBuilder<SubscriptionRequest> builder)
    {
        builder.Property(e => e.ReportPath).HasMaxLength(500);
    }
}
