BEGIN TRANSACTION;
GO

ALTER TABLE [SubscriptionRequests] ADD [Description] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [SubscriptionRequests] ADD [SubscriptionType] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240702202148_AddedColumnsSubscriptionTypeAndDescriptionToTableSubscriptionRequests', N'8.0.6');
GO

COMMIT;
GO

