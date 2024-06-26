BEGIN TRANSACTION;
GO

ALTER TABLE [SubscriptionRequests] ADD [DeliveryOption] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240625082008_AddedColumnDeliveryOptionToTableSubscriptionRequests', N'8.0.6');
GO

COMMIT;
GO

