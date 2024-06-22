using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;

public class FileShareDeliveryOption : IJsonEntity
{
    public RenderFormat RenderFormat { get; set; }

    public required string FileName { get; set; }

    public bool IncludeExtension { get; set; }

    public required string Path { get; set; }

    public OverrideOptions OverrideOptions { get; set; }
}
