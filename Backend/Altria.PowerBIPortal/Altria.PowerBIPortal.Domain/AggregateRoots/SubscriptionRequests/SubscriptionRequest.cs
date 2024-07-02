using Altria.PowerBIPortal.Domain.AggregateRoots.Identity.Entities;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;
using Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;

public class SubscriptionRequest : ApprovalRequest<SubscriptionRequestApprovalLevel>
{
    private SubscriptionRequest()
    {
    }

    public required string ReportPath { get; set; }

    public required string Owner { get; set; }

    public required string Description { get; set; }

    public SubscriptionType SubscriptionType { get; set; }

    public required SubscriptionInfo SubscriptionInfo { get; set; }

    public required DeliveryOption DeliveryOption { get; set; }

    public ScheduleType ScheduleType { get; set; }

    public required Schedule Schedule { get; set; }

    public Guid? SharedScheduleReference { get; set; }

    public bool IsProcessed { get; private set; }

    public Guid? SubscriptionReference { get; private set; }

    public static SubscriptionRequest Create(string reportPath, string owner, string description, SubscriptionType subscriptionType, SubscriptionInfo subscriptionInfo, ScheduleType scheduleType, Schedule schedule, DeliveryOption deliveryOption, User requester)
    {
        switch (scheduleType)
        {
            case ScheduleType.HourlySchedule:
                schedule.DailySchedule = null;
                schedule.WeeklySchedule = null;
                schedule.MonthlySchedule = null;
                schedule.OneTimeSchedule = null;
                break;

            case ScheduleType.DailySchedule:
                schedule.HourlySchedule = null;
                schedule.WeeklySchedule = null;
                schedule.MonthlySchedule = null;
                schedule.OneTimeSchedule = null;
                break;

            case ScheduleType.WeeklySchedule:
                schedule.HourlySchedule = null;
                schedule.DailySchedule = null;
                schedule.MonthlySchedule = null;
                schedule.OneTimeSchedule = null;
                break;

            case ScheduleType.MonthlySchedule:
                schedule.HourlySchedule = null;
                schedule.DailySchedule = null;
                schedule.WeeklySchedule = null;
                schedule.OneTimeSchedule = null;
                break;

            case ScheduleType.OneTimeSchedule:
                schedule.HourlySchedule = null;
                schedule.DailySchedule = null;
                schedule.WeeklySchedule = null;
                schedule.MonthlySchedule = null;
                break;
        }

        switch (subscriptionType)
        {
            case SubscriptionType.StandardSubscription:
                subscriptionInfo.DataDrivenSubscription = null;
                break;

            case SubscriptionType.DataDrivenSubscription:
                subscriptionInfo.StandardSubscription = null;
                break;
        }

        var subscription = new SubscriptionRequest
        {
            Requester = requester,
            ReportPath = reportPath,
            Owner = owner,
            Description = description,
            SubscriptionType = subscriptionType,
            SubscriptionInfo = subscriptionInfo,
            DeliveryOption = deliveryOption,
            ScheduleType = scheduleType,
            Schedule = schedule
        };

        subscription.ApprovalRequestLevels.Add(new SubscriptionRequestApprovalLevel { SubscriptionRequest = subscription, ApprovalLevel = 1 });

        return subscription;
    }

    public new void Approved(User approvalOfficer, SubscriptionRequestApprovalLevel currentApprovalLevel)
    {
        base.Approved(approvalOfficer, currentApprovalLevel);

        switch (currentApprovalLevel.ApprovalLevel)
        {
            case 1:
                ApprovalRequestLevels.Add(new SubscriptionRequestApprovalLevel { SubscriptionRequest = this, ApprovalLevel = currentApprovalLevel.ApprovalLevel + 1 });
                break;

            case 2:
                Status = ApprovalStatus.Approved;
                break;
        }
    }

    internal void Processed(Guid subscritionId)
    {
        if (IsProcessed)
        {
            return;
        }

        IsProcessed = true;
        SubscriptionReference = subscritionId;
    }
}
