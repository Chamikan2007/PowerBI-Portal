
import { KeyValue } from "@angular/common";
import { SubscriptionDetailsMeta } from "app/subscriptions/subscription-details/subscription-details.meta";

export class SubscriptionDto {
    subscriptionId: string = '';
    report: ReportDto = new ReportDto();
    description: string = '';
    owner: string = 'SampathBnakHema\\Hemantha';

    subscriptionType: number = 1;
    destination: number = 2;
    deliveryOptionEmail: DeliveryOptionEmailDto = new DeliveryOptionEmailDto();
    scheduleDetailType: number = 2;
    scheduleType: number = 1;
    schedule: ScheduleDto = new ScheduleDto();

    status: ApprovalStatus = ApprovalStatus.None;
    requesterName: string = '';
    requesterId: string = '';
    approvalLevels: ApproverLevelDto[] = [];
}

export class ScheduleDto {
    scheduleDetailHourly = new ScheduleTypeHourlyDto(); // hourly, daily, weekly, monthly, onetime
    scheduleDetailDaily = new ScheduleTypeDailyDto(); // hourly, daily, weekly, monthly, onetime
    scheduleDetailWeekly = new ScheduleTypeWeeklyDto(); // hourly, daily, weekly, monthly, onetime
    scheduleDetailMonthly = new ScheduleTypeMonthlyDto(); // hourly, daily, weekly, monthly, onetime
    scheduleDetailOneTime = new ScheduleTypeOneTimeDto(); // hourly, daily, weekly, monthly, onetime
}

export class ReportDto {
    name: string = '';
    path: string = ''
}

export class DeliveryOptionEmailDto {
    to: string = '';
    cc: string = '';
    bcc: string = '';
    replyTo: string = '';
    subject: string = '';
    includeReport: boolean = true;
    inlcudeLink: boolean = true;
    renderFormat: number = 1;
    priority: number = 1;
    comment: string = '';
}

export class StartTimeDto {
    startHour: number = 0;
    startMinute: number = 0;
    meridiem: number = 1;

    startDateTime: Date = new Date();
    endDate?: Date;
}

export class ScheduleTypeHourlyDto extends StartTimeDto {
    hour: number = 0;
    minute: number = 0;
}

export class ScheduleTypeDailyDto extends StartTimeDto {
    dailyScheduleType: number = 1;
    selectedDays: KeyValue<string, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.value, value: true }) as KeyValue<string, boolean>));
    repeatAfterDaysCount: number = 0;
}

export class ScheduleTypeWeeklyDto extends StartTimeDto {
    repeatAfterDaysCount: number = 0;
    selectedDays: KeyValue<string, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.value, value: true }) as KeyValue<string, boolean>));
}

export class ScheduleTypeMonthlyDto extends StartTimeDto {
    selectedMonths: KeyValue<string, boolean>[] = Array.from(SubscriptionDetailsMeta.monthsOfYearItems.map(d => ({ key: d.value, value: true }) as KeyValue<string, boolean>));
    monthlyScheduleType: number = 1;
    onWeekOfMonth: number = 1;
    onDaysOfWeek: KeyValue<string, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.value, value: true }) as KeyValue<string, boolean>));
    onCalendarDays: string = '';
}

export class ScheduleTypeOneTimeDto extends StartTimeDto { }




export class ApproverLevelDto {
    approvalLevelId: string = '';
    status: ApprovalStatus = ApprovalStatus.None;
    approvalOfficerId: string = '';
    approvalOfficerName: string = '';
    approvalLevel: number = 0;
    comment: string = '';
}

export enum ApprovalLevels {
    None = 0,
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Cancelled = 4,
}

export enum ApprovalStatus {
    None = 0,
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Cancelled = 4,
}




