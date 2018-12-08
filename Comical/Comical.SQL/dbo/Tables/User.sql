CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Login NVARCHAR(70),
	Password NVARCHAR(70),
	Enabled BIT DEFAULT (0),
	Blocked BIT DEFAULT (1),
	Retries INT DEFAULT (0),
	__HorizontalVerifier__ NVARCHAR(MAX)
)
