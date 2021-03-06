﻿CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	UserId INT NOT NULL,
	RoleId INT NOT NULL,
	__HorizontalVerifier__ NVARCHAR(MAX),
	FOREIGN KEY (UserId) REFERENCES [User],
	FOREIGN KEY (RoleId) REFERENCES Role
)
