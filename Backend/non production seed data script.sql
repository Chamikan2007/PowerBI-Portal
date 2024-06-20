USE [PowerBIPortal]
GO
INSERT [dbo].[Users] ([Id], [Name], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9ba3d7a1-7c8a-4ced-c8b5-08dc8ed97dc7', N'SSRS Testuser1', N'ssrs.test1@thealtria.com', N'SSRS.TEST1@THEALTRIA.COM', N'ssrs.test1@thealtria.com', N'SSRS.TEST1@THEALTRIA.COM', 0, NULL, N'52JUWGAVCXSYJ34USCNDUNYS7RNTZIEU', N'cee21c76-a700-4938-abe4-b831bc672481', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[Users] ([Id], [Name], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3d12ceed-0b53-449f-f868-08dc8eec0ee2', N'Hemantha Kumara', N'hemantha.k@thealtria.com', N'HEMANTHA.K@THEALTRIA.COM', N'hemantha.k@thealtria.com', N'HEMANTHA.K@THEALTRIA.COM', 0, NULL, N'VY2BPN4NSROX366RIUXJ2UBBHCSWPUEE', N'ebb0360c-a4bf-4d1f-a0a6-e56da547f6ba', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[ApprovalOfficers] ([Id], [ApprovalRequestType], [ApprovalLevel], [OfficerId], [CreatedAtUtc], [CreatedBy], [UpdatedAtUtc], [UpdatedBy]) VALUES (N'00000000-0000-0000-0000-000000000000', 1, 1, N'3d12ceed-0b53-449f-f868-08dc8eec0ee2', CAST(N'2016-06-19T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000', CAST(N'2016-06-19T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[ApprovalOfficers] ([Id], [ApprovalRequestType], [ApprovalLevel], [OfficerId], [CreatedAtUtc], [CreatedBy], [UpdatedAtUtc], [UpdatedBy]) VALUES (N'00000000-0000-0000-0000-000000000001', 1, 2, N'3d12ceed-0b53-449f-f868-08dc8eec0ee2', CAST(N'2016-06-19T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000', CAST(N'2016-06-19T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'LDAP', N'hemantha.k', N'LDAP', N'3d12ceed-0b53-449f-f868-08dc8eec0ee2')
GO
INSERT [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'LDAP', N'ssrs.test1', N'LDAP', N'9ba3d7a1-7c8a-4ced-c8b5-08dc8ed97dc7')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'3d12ceed-0b53-449f-f868-08dc8eec0ee2', N'8417099f-6985-4877-b123-c6bf46c306b5')
GO
INSERT [dbo].[SubscriberWhiteList] ([Id], [WhiteListEntry], [EntryType], [CreatedAtUtc], [CreatedBy], [UpdatedAtUtc], [UpdatedBy]) VALUES (N'00000000-0000-0000-0000-000000000011', N'gmail.com', 1, CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000', CAST(N'2024-06-16T00:00:00.0000000' AS DateTime2), N'00000000-0000-0000-0000-000000000000')
GO
