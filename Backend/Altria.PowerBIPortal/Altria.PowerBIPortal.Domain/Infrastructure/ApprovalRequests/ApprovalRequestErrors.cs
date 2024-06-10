namespace Altria.PowerBIPortal.Domain.Infrastructure.ApprovalRequests;

public static class ApprovalRequestErrors
{
    public static Error InvalidAction => new("ApprovalRequests.InvalidAction");

    public static Error CommentCannotBeEmpty => new("ApprovalRequests.CommentCannotBeEmpty");

    public static Error InvalidRequest => new("ApprovalRequests.InvalidRequest");

    public static Error InvalidStatus => new("ApprovalRequests.InvalidStatus");
}
