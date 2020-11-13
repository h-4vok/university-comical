CREATE TABLE [dbo].[HistoryException]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Section NVARCHAR(70) NOT NULL,
	ExceptionType NVARCHAR(300) NOT NULL,
	ExceptionSource NVARCHAR(300),
	ExceptionMessage NVARCHAR(700) NOT NULL,
	ExceptionStackTrace NVARCHAR(MAX),
	UserId INT,
	DateLogged DATETIME DEFAULT GETUTCDATE(),
	__HorizontalVerifier__ NVARCHAR(MAX),
)
