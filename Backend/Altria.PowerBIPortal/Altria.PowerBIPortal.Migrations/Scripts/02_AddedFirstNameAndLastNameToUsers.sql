BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [FirstName] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Users] ADD [LastName] nvarchar(50) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240611190021_AddedFirstNameAndLastNameToUsers', N'8.0.6');
GO

COMMIT;
GO

