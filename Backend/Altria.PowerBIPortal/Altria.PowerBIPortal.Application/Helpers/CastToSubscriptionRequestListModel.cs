using Altria.PowerBIPortal.Application.Endpoints.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

namespace Altria.PowerBIPortal.Application.Helpers;

public static class CastToSubscriptionRequestListModel
{
    internal static SubscriptionRequestListModel Cast(SubscriptionRequest subscriptionRequest)
    {
        var isStandardSubscription = subscriptionRequest.SubscrptionInfo.StandardSubscription != null;

        var description = isStandardSubscription ?
            subscriptionRequest.SubscrptionInfo.StandardSubscription!.Description :
            subscriptionRequest.SubscrptionInfo.DataDrivenSubscription!.Description;

        var model = new SubscriptionRequestListModel
        {
            SubscriptionId = subscriptionRequest.Id,
            Description = description,
            ReportPath = subscriptionRequest.ReportPath,
            RequesterId = subscriptionRequest.Requester.Id,
            RequesterName = subscriptionRequest.Requester.Name,
            Status = subscriptionRequest.Status,
        };

        return model;
    }
}
