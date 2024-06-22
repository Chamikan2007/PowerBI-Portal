using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Abstractions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Altria.PowerBIPortal.Persistence.Configurations.SubscriptionRequests;

internal class SubscriptionRequestConfiguration : IEntityTypeConfiguration<SubscriptionRequest>
{
    public void Configure(EntityTypeBuilder<SubscriptionRequest> builder)
    {
        builder.Property(e => e.ReportPath).HasMaxLength(500);
        //builder.OwnsOne(e => e.SubscrptionInfo, builder => builder.ToJson());
        //builder.OwnsOne(e => e.Schedule, builder => builder.ToJson());

        builder.Property(e => e.SubscrptionInfo)
            .HasConversion(new ValueConverter<SubscrptionInfo, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<SubscrptionInfo>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!))
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Schedule)
            .HasConversion(new ValueConverter<Schedule?, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Schedule>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!))
            .HasColumnType("nvarchar(max)");

    }
}
