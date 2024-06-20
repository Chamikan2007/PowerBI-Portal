using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriberWhiteListEntries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriberWhiteListEntries;

public class SubscriberWhiteListConfiguration : IEntityTypeConfiguration<SubscriberWhiteList>
{
    public void Configure(EntityTypeBuilder<SubscriberWhiteList> builder)
    {
        builder.ToTable(typeof(SubscriberWhiteList).Name);

        builder.HasIndex(e => e.WhiteListEntry).IsUnique();
        builder.HasIndex(e => e.EntryType);
    }
}
