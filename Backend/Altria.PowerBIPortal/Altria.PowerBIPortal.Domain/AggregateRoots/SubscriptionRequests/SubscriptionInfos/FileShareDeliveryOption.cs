using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class FileShareDeliveryOption
{
    public RenderFormat RenderFormat { get; set; }

    public required string FileName { get; set; }

    public bool IncludeExtension { get; set; }

    public required string Path { get; set; }

    public OverrideOptions OverrideOptions { get; set; }
}
