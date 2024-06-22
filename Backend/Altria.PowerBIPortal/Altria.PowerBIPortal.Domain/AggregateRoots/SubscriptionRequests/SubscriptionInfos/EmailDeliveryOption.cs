using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Abstractions;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class EmailDeliveryOption
{
    public RenderFormat RenderFormat { get; set; }

    public required string To { get; set; }
 
    public string? Cc { get; set; }

    public string? Bcc { get; set; }
 
    public string? ReplyTo { get; set; }

    public required string Subject { get; set; }

    public bool IncludeReport { get; set; }

    public bool IncludeLink { get; set; }

    public Prioriry Prioriry { get; set; }

    public string? Comment { get; set; }
}
