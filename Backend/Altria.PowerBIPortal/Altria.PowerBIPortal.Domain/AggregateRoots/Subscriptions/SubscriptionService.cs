using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.Schedules.Enums;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos;
using Altria.PowerBIPortal.Domain.AggregateRoots.SubscriptionRequests.SubscriptionInfos.Enums;
using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.DomainServices;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService.Entities;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRequestRepository _subscriptionRepository;
    private readonly IPowerBIReportService _powerBIReportService;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(ISubscriptionRequestRepository subscriptionRepository, IPowerBIReportService powerBIReportService, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _powerBIReportService = powerBIReportService;
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessSubscriptionRequestsAsync()
    {
        var subscriptionRequests = await _subscriptionRepository.FetchSubscriptionRequestsToProcessAsync();

        foreach (var subscriptionRequest in subscriptionRequests)
        {
            var subscription = CastTpBowerBISubScription(subscriptionRequest);
            //var subscritionId = await _powerBIReportService.CreateSubscriptionAsync(subscription);
            //subscriptionRequest.Processed(subscritionId);

            //await _unitOfWork.SaveChangesAsync();
        }
    }

    private Subscription CastTpBowerBISubScription(SubscriptionRequest subscriptionRequest)
    {
        var subscription = (subscriptionRequest.SubscriptionInfo.StandardSubscription != null) ?
            CastFromStandardSubscription(subscriptionRequest.SubscriptionInfo.StandardSubscription) :
            CastFromDataDrivenSubscription(subscriptionRequest.SubscriptionInfo.DataDrivenSubscription);

        // _powerBIReportService.GetReportsByIdAsync(subscriptionRequest.ReportPath);

        subscription.Report = subscriptionRequest.ReportPath;

        SetDeliveryOption(subscription, subscriptionRequest.DeliveryOption);

        if (subscriptionRequest.Schedule != null)
        {
            SetSchedule(subscription, subscriptionRequest.Schedule, subscriptionRequest.ScheduleType);
        }

        return subscription;
    }

    private Subscription CastFromStandardSubscription(StandardSubscription standardSubscription)
    {
        var subscription = new Subscription
        {
            Description = standardSubscription.Description,
            EventType = ReportServerEventType.TimedSubscription.ToString(),
            IsDataDriven = false,
        };

        return subscription;
    }

    private Subscription CastFromDataDrivenSubscription(DataDrivenSubscription? dataDrivenSubscription)
    {
        throw new NotImplementedException();
    }

    private void SetDeliveryOption(Subscription subscription, DeliveryOption deliveryOption)
    {
        _ = deliveryOption.EmailDeliveryOption != null ?
                SetEmailDeliveryOption(subscription, deliveryOption.EmailDeliveryOption) :
                SetFileShareDeliveryOption(subscription, deliveryOption.FileShareDeliveryOption);
    }

    private List<ParameterValue> SetEmailDeliveryOption(Subscription subscription, EmailDeliveryOption emailDeliveryOption)
    {
        subscription.DeliveryExtension = "Report Server Email";
        subscription.ParameterValues = new List<ParameterValue>()
        {
            new() {
                Name = "TO",
                Value = emailDeliveryOption.To,
            },
            new() {
                Name = "IncludeReport",
                Value = emailDeliveryOption.IncludeReport.ToString(),
            },
            new() {
                Name = "RenderFormat",
                Value = GetRenderFormatString(emailDeliveryOption.RenderFormat),
            },
            new() {
                Name = "Subject",
                Value = emailDeliveryOption.Subject,
            },
            new() {
                Name = "IncludeLink",
                Value = emailDeliveryOption.IncludeLink.ToString(),
            },
            new() {
                Name = "Priority",
                Value = emailDeliveryOption.Prioriry.ToString().ToUpper(),
            }
        };

        if (emailDeliveryOption.Cc != null)
        {
            subscription.ParameterValues.Add(new ParameterValue()
            {
                Name = "CC",
                Value = emailDeliveryOption.Cc,
            });
        }

        if (emailDeliveryOption.Bcc != null)
        {
            subscription.ParameterValues.Add(new ParameterValue()
            {
                Name = "BCC",
                Value = emailDeliveryOption.Bcc,
            });
        }

        if (emailDeliveryOption.ReplyTo != null)
        {
            subscription.ParameterValues.Add(new ParameterValue()
            {
                Name = "ReplyTo",
                Value = emailDeliveryOption.ReplyTo,
            });
        }

        if (emailDeliveryOption.Comment != null)
        {
            subscription.ParameterValues.Add(new ParameterValue()
            {
                Name = "Comment",
                Value = emailDeliveryOption.Comment,
            });
        }

        return subscription.ParameterValues;
    }

    private List<ParameterValue> SetFileShareDeliveryOption(Subscription subscription, FileShareDeliveryOption? fileShareDeliveryOption)
    {
        throw new NotImplementedException();
    }

    private string GetRenderFormatString(RenderFormat renderFormat)
    {
        return renderFormat switch
        {
            RenderFormat.Word => "WORDOPENXML",
            RenderFormat.Excel => "EXCELOPENXML",
            RenderFormat.PowerPoint => "PPTX",
            RenderFormat.Pdf => "PDF",
            RenderFormat.AccessiblePdf => "AccessiblePDF",
            RenderFormat.Tiff => "IMAGE",
            RenderFormat.Mhtml => "MHTML",
            RenderFormat.Csv => "CSV",
            RenderFormat.Xml => "XML",
            RenderFormat.DataFeed => "ATOM",
            _ => throw new NotImplementedException()
        };
    }

    private void SetSchedule(Subscription subscription, SubscriptionRequests.Schedules.Schedule schedule, ScheduleType scheduleType)
    {
        subscription.Schedule = scheduleType switch
        {
            ScheduleType.HourlySchedule => SetHourlySchedule(schedule.HourlySchedule!),
            ScheduleType.DailySchedule => SetDailySchedule(schedule.DailySchedule!),
            ScheduleType.WeeklySchedule => SetWeeklySchedule(schedule.WeeklySchedule!),
            ScheduleType.MonthlySchedule => SetMonthlySchedule(schedule.MonthlySchedule!),
            ScheduleType.OneTimeSchedule => SetOneTimeSchedule(schedule.OneTimeSchedule!),
            _ => throw new NotImplementedException()
        };
    }

    private Contracts.IPowerBIService.Entities.Schedule SetHourlySchedule(HourlySchedule hourlySchedule)
    {
        var schedule = SetCommonScheduleProperties(hourlySchedule);

        schedule.Definition.Recurrence = new Recurrence
        {
            MinuteRecurrence = new MinuteRecurrence
            {
                MinutesInterval = hourlySchedule.Hours * 60 + hourlySchedule.Minutes,
            }
        };

        return schedule;
    }

    private Contracts.IPowerBIService.Entities.Schedule SetDailySchedule(DailySchedule dailySchedule)
    {
        var schedule = SetCommonScheduleProperties(dailySchedule);

        schedule.Definition.Recurrence = new Recurrence
        {
            DailyRecurrence = new 
            {
            }
        };

        return schedule;
    }

    private Contracts.IPowerBIService.Entities.Schedule SetWeeklySchedule(WeeklySchedule weeklySchedule)
    {
        var schedule = SetCommonScheduleProperties(weeklySchedule);

        schedule.Definition.Recurrence = new Recurrence
        {
            WeeklyRecurrence = new
            {
            }
        };

        return schedule;
    }

    private Contracts.IPowerBIService.Entities.Schedule SetMonthlySchedule(MonthlySchedule monthlySchedule)
    {
        var schedule = SetCommonScheduleProperties(monthlySchedule);

        schedule.Definition.Recurrence = new Recurrence
        {
            MonthlyRecurrence = new
            {
            }
        };

        return schedule;
    }

    private Contracts.IPowerBIService.Entities.Schedule SetOneTimeSchedule(OneTimeSchedule oneTimeSchedule)
    {
        var schedule = SetCommonScheduleProperties(oneTimeSchedule);

        schedule.Definition.Recurrence = new Recurrence
        {
            MonthlyDOWRecurrence = new
            {
            }
        };

        return schedule;
    }

    private Contracts.IPowerBIService.Entities.Schedule SetCommonScheduleProperties(ISchedule schedule)
    {
        var powerBISchedule = new Contracts.IPowerBIService.Entities.Schedule
        {
            Definition = new Definition
            {
                StartDateTime = schedule.StartDateTime,
                EndDateSpecified = schedule.StoptDate.HasValue,
            }
        };

        if (schedule.StoptDate.HasValue)
        {
            powerBISchedule.Definition.EndDate = schedule.StoptDate.Value.Date;
        }

        return powerBISchedule;
    }
}
