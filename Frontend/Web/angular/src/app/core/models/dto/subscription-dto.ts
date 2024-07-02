
import { KeyValue } from "@angular/common";
import { SubscriptionDetailsMeta } from "app/subscriptions/subscription-details/subscription-details.meta";

export interface SubscriptionListModel {
    subscriptionId: string;
    description: string;
    reportPath: string;
    status: ApprovalStatus;
    requesterName: string;
    requesterId: string
}

export interface SubscriptionRequestApproverLevelModel {
    approvalLevelId: string;
    status: ApprovalStatus;
    approvalOfficerId: string;
    approvalOfficerName: string;
    approvalLevel: number;
    comment: string,
    approvedAtUtc: Date;
}

export class SubscriptionDto {
    subscriptionId: string = '';
    report: ReportDto = new ReportDto();
    description: string = '';
    subscriptionType: number = 1;
    destination: number = 2;
    scheduleDetailType: number = 2;
    scheduleType: number = 1;
    status: ApprovalStatus = ApprovalStatus.None;
    requesterName: string = '';
    requesterId: string = '';
    deliveryOption: DeliveryOption = new DeliveryOption();
    schedule: ScheduleDto = new ScheduleDto();
    subscriptionInfo: SubscriptionInfo = new SubscriptionInfo();
}

export class ScheduleDto {
    hourlySchedule = new ScheduleTypeHourlyDto(); // hourly
    // dailySchedule = new ScheduleTypeDailyDto(); // daily
    // weeklySchedule = new ScheduleTypeWeeklyDto(); // weekly
    // monthlySchedule = new ScheduleTypeMonthlyDto(); // monthly
    // oneTimeSchedule = new ScheduleTypeOneTimeDto(); // onetime
}

export class ReportDto {
    name: string = '';
    path: string = '';
    owner: string = '';
}

export class DeliveryOption {
    emailDeliveryOption: DeliveryOptionEmailDto = new DeliveryOptionEmailDto();
    // fileShareDeliveryOption: FileShareDeliveryOption = new FileShareDeliveryOption();
}

export class StandardSubscription {
    // description: string = '1';
}

export class DataDrivenSubscription {
    description: string = '';
}

export class SubscriptionInfo {
    standardSubscription: StandardSubscription = new StandardSubscription();
    // dataDrivenSubscription: DataDrivenSubscription = new DataDrivenSubscription();
}

export class DeliveryOptionEmailDto {
    to: string = '';
    cc: string = '';
    bcc: string = '';
    replyTo: string = '';
    subject: string = '';
    includeReport: boolean = true;
    includeLink: boolean = true;
    renderFormat: number = 1;
    priority: number = 1;
    comment: string = '';
}

export class FileShareDeliveryOption {
    renderFormat: number = 0;
    fileName: string = "";
    includeExtension: Boolean = true;
    path: string = "";
    overrideOptions: number = 0;
}

export class StartTimeDto {
    startDateTime: Date = new Date();
    stoptDate: Date = new Date();
    isDisabledStopDate: boolean = true;
}

export class ScheduleTypeHourlyDto extends StartTimeDto {
    hours: number = 0;
    minutes: number = 0;
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