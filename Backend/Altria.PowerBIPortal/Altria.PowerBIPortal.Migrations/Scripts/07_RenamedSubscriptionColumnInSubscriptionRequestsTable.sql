BEGIN TRANSACTION;
GO

EXEC sp_rename N'[SubscriptionRequests].[SubscrptionInfo]', N'SubscriptionInfo', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240702192726_RenamedSubscriptionColumnInSubscriptionRequestsTable', N'8.0.6');
GO

COMMIT;
GO

