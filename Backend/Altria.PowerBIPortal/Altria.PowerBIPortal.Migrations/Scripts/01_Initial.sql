IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NULL,
    [NormalizedName] nvarchar(50) NULL,
    [ConcurrencyStamp] nvarchar(50) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [UserName] nvarchar(50) NULL,
    [NormalizedUserName] nvarchar(50) NULL,
    [Email] nvarchar(50) NULL,
    [NormalizedEmail] nvarchar(50) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(50) NULL,
    [SecurityStamp] nvarchar(50) NULL,
    [ConcurrencyStamp] nvarchar(50) NULL,
    [PhoneNumber] nvarchar(50) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(50) NULL,
    [ClaimValue] nvarchar(50) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
);
GO

CREATE TABLE [SubscriptionRequests] (
    [Id] uniqueidentifier NOT NULL,
    [Report] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [Status] int NOT NULL,
    [RequesterId] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    CONSTRAINT [PK_SubscriptionRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubscriptionRequests_Users_RequesterId] FOREIGN KEY ([RequesterId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(50) NULL,
    [ClaimValue] nvarchar(50) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(50) NOT NULL,
    [ProviderKey] nvarchar(50) NOT NULL,
    [ProviderDisplayName] nvarchar(50) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(50) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Value] nvarchar(50) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [SubscriptionApprovalStep] (
    [Id] uniqueidentifier NOT NULL,
    [SubscriptionRequestId] uniqueidentifier NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [Status] int NOT NULL,
    [ApprovalOfficerId] uniqueidentifier NULL,
    [StepIndex] int NOT NULL,
    [Comment] nvarchar(500) NULL,
    CONSTRAINT [PK_SubscriptionApprovalStep] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubscriptionApprovalStep_SubscriptionRequests_SubscriptionRequestId] FOREIGN KEY ([SubscriptionRequestId]) REFERENCES [SubscriptionRequests] ([Id]),
    CONSTRAINT [FK_SubscriptionApprovalStep_Users_ApprovalOfficerId] FOREIGN KEY ([ApprovalOfficerId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_SubscriptionApprovalStep_ApprovalOfficerId] ON [SubscriptionApprovalStep] ([ApprovalOfficerId]);
GO

CREATE INDEX [IX_SubscriptionApprovalStep_SubscriptionRequestId] ON [SubscriptionApprovalStep] ([SubscriptionRequestId]);
GO

CREATE INDEX [IX_SubscriptionRequests_RequesterId] ON [SubscriptionRequests] ([RequesterId]);
GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240614074955_Initial', N'8.0.6');
GO

COMMIT;
GO

