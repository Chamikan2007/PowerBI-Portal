BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SubscriptionRequests]') AND [c].[name] = N'Schedule');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SubscriptionRequests] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [SubscriptionRequests] SET [Schedule] = N'' WHERE [Schedule] IS NULL;
ALTER TABLE [SubscriptionRequests] ALTER COLUMN [Schedule] nvarchar(max) NOT NULL;
ALTER TABLE [SubscriptionRequests] ADD DEFAULT N'' FOR [Schedule];
GO

ALTER TABLE [SubscriptionRequests] ADD [ScheduleType] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240702135712_AddedScheduleTypeToTableSubscriptionRequests', N'8.0.6');
GO

COMMIT;
GO

