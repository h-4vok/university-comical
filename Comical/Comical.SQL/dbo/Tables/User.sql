CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Login NVARCHAR(70),
	Password NVARCHAR(70),
	Enabled BIT,
	Blocked BIT
)
