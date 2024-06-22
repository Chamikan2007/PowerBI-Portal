using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class DeliveryOption : IJsonEntity
{
    public EmailDeliveryOption? EmailDeliveryOption { get; set; }

    public FileShareDeliveryOption? FileShareDeliveryOption { get; set; }
}
