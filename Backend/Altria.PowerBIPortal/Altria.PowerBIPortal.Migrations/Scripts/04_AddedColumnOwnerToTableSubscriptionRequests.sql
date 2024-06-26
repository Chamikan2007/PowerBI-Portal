BEGIN TRANSACTION;
GO

ALTER TABLE [SubscriptionRequests] ADD [Owner] nvarchar(50) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240625094059_AddedColumnOwnerToTableSubscriptionRequests', N'8.0.6');
GO

COMMIT;
GO

