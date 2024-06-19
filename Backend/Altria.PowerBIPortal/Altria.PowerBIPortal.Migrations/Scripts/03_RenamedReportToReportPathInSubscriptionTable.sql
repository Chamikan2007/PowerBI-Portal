BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Subscription]') AND [c].[name] = N'Report');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Subscription] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Subscription] DROP COLUMN [Report];
GO

ALTER TABLE [Subscription] ADD [ReportPath] nvarchar(500) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240619110452_RenamedReportToReportPathInSubscriptionTable', N'8.0.6');
GO

COMMIT;
GO

