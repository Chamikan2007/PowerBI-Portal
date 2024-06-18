export class SubscriptionDto {
    subscriptionId: string = '';
    report: string = '';
    email: string = '';
    status: number = 0;
    requesterName: string = '';
    requesterId: string = '';
    approvalLevels: ApproverLevelDto[] = [];
}

export class ApproverLevelDto {
    approvalLevelId: string = '';
    status: number = 0;
    approvalOfficerId: string = '';
    approvalOfficerName: string = '';
    approvalLevel: number = 0;
    comment: string = '';
}