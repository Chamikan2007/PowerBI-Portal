using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionWhiteList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriptionWhiteList;

public class SubscriptionWhiteListEntryConfiguration : IEntityTypeConfiguration<SubscriptionWhiteListEntry>
{
    public void Configure(EntityTypeBuilder<SubscriptionWhiteListEntry> builder)
    {
        builder.HasIndex(e => e.WhiteListEntry).IsUnique();
        builder.HasIndex(e => e.EntryType);
    }
}
