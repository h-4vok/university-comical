﻿CREATE TABLE [dbo].[HistoryEvent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Section NVARCHAR(70) NOT NULL,
	Message NVARCHAR(300) NOT NULL,
	UserId INT NULL,
	DateLogged DATETIME DEFAULT GETUTCDATE(),
	__HorizontalVerifier__ NVARCHAR(MAX),
)
