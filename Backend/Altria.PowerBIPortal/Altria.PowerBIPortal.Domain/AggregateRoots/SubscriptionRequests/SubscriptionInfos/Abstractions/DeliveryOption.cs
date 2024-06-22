using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;

public class DeliveryOption
{
    public EmailDeliveryOption? EmailDeliveryOption { get; set; }

    public FileShareDeliveryOption? FileShareDeliveryOption { get; set; }
}
