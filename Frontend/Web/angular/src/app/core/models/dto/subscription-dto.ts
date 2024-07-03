
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
    subscriptionType: number = 0;
    destination: number = 1;
    scheduleDetailType: number = 1;
    scheduleType: number = 1;
    status: ApprovalStatus = ApprovalStatus.None;
    requesterName: string = '';
    requesterId: string = '';
    deliveryOption: DeliveryOption = new DeliveryOption();
    schedule: ScheduleDto = new ScheduleDto();
    subscriptionInfo: SubscriptionInfo = new SubscriptionInfo();
    isDisabledStopDate: boolean = true;
}

export class ScheduleDto {
    hourlySchedule: any = new ScheduleTypeHourlyDto(); // hourly
    dailySchedule: any = new ScheduleTypeDailyDto(); // daily
    weeklySchedule: any = new ScheduleTypeWeeklyDto(); // weekly
    monthlySchedule: any = new ScheduleTypeMonthlyDto(); // monthly
    oneTimeSchedule: any = new ScheduleTypeOneTimeDto(); // onetime
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
    renderFormat: number = 0;
    priority: number = 0;
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
    selectedTime: Date = new Date();
}

export class ScheduleTypeHourlyDto extends StartTimeDto {
    hours: number = 0;
    minutes: number = 0;
}

export class ScheduleTypeDailyDto extends StartTimeDto {
    dailyScheduleType: number = 0;
    selectedDays: KeyValue<number, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.key, value: true }) as KeyValue<number, boolean>));
    repeatAfterDaysCount: number = 0;
}

export class ScheduleTypeWeeklyDto extends StartTimeDto {
    repeatAfterDaysCount: number = 0;
    selectedDays: KeyValue<number, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.key, value: true }) as KeyValue<number, boolean>));
}

export class ScheduleTypeMonthlyDto extends StartTimeDto {
    selectedMonths: KeyValue<number, boolean>[] = Array.from(SubscriptionDetailsMeta.monthsOfYearItems.map(d => ({ key: d.key, value: true }) as KeyValue<number, boolean>));
    monthlyScheduleType: number = 0;
    onWeekOfMonth: number = 0;
    onDaysOfWeek: KeyValue<number, boolean>[] = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.key, value: true }) as KeyValue<number, boolean>));
    onCalendarDays: string = '';
}

export class ScheduleTypeOneTimeDto extends StartTimeDto {}

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