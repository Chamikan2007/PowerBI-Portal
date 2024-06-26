BEGIN TRANSACTION;
GO

ALTER TABLE [SubscriptionRequestApprovalLevels] DROP CONSTRAINT [FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionId];
GO

EXEC sp_rename N'[SubscriptionRequestApprovalLevels].[SubscriptionId]', N'SubscriptionRequestId', N'COLUMN';
GO

EXEC sp_rename N'[SubscriptionRequestApprovalLevels].[IX_SubscriptionRequestApprovalLevels_SubscriptionId]', N'IX_SubscriptionRequestApprovalLevels_SubscriptionRequestId', N'INDEX';
GO

ALTER TABLE [SubscriptionRequestApprovalLevels] ADD CONSTRAINT [FK_SubscriptionRequestApprovalLevels_SubscriptionRequests_SubscriptionRequestId] FOREIGN KEY ([SubscriptionRequestId]) REFERENCES [SubscriptionRequests] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626163912_RenamedForeignKeyToSubscriptionRequets', N'8.0.6');
GO

COMMIT;
GO

