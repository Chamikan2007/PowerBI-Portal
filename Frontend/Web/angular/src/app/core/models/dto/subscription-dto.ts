export class SubscriptionDto {
    subscriptionId: string = '';
    report: ReportDto = new ReportDto();
    email: string = '';
    status: ApprovalStatus = ApprovalStatus.None;
    requesterName: string = '';
    requesterId: string = '';
    approvalLevels: ApproverLevelDto[] = [];
}

export class ReportDto {
    name: string = '';
    path: string = ''
}

export class ApproverLevelDto {
    approvalLevelId: string = '';
    status: ApprovalStatus = ApprovalStatus.None;
    approvalOfficerId: string = '';
    approvalOfficerName: string = '';
    approvalLevel: number = 0;
    comment: string = '';
}

export enum ApprovalStatus {
    None = 0,
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Cancelled = 4,
}
