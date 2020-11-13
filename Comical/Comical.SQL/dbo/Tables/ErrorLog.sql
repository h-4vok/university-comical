CREATE TABLE [dbo].[ErrorLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	ExceptionMessage NVARCHAR(700) NOT NULL,
	ExceptionType NVARCHAR(300) NOT NULL,
	ExceptionSource NVARCHAR(300),
	ExceptionStackTrace NVARCHAR(MAX),
	LoggedBy INT NULL,
	LoggedDate DATETIME NOT NULL DEFAULT GETUTCDATE(),

	FOREIGN KEY (LoggedBy) REFERENCES [User]
)
