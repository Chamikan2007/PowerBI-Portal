using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
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

        builder.Property(e => e.SubscriptionInfo)
            .HasConversion(new ValueConverter<SubscriptionInfo, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<SubscriptionInfo>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!))
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.DeliveryOption)
            .HasConversion(new ValueConverter<DeliveryOption, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<DeliveryOption>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!))
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Schedule)
            .HasConversion(new ValueConverter<Schedule?, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Schedule>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!))
            .HasColumnType("nvarchar(max)");

    }
}
