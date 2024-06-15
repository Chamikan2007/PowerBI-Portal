using Altria.PowerBIPortal.Domain.AggregateRoots.ApprovalConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altria.PowerBIPortal.Persistence.Configurations.ApprovalConfigs;

internal class ApprovalOfficerConfiguration : IEntityTypeConfiguration<ApprovalOfficer>
{
    public void Configure(EntityTypeBuilder<ApprovalOfficer> builder)
    {
        builder.HasIndex(e => new { e.ApprovalRequestType, e.ApprovalLevel });
    }
}
