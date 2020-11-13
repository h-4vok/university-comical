CREATE TABLE [dbo].[Role]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Code NVARCHAR(70),
	Description NVARCHAR(70),
	Enabled BIT,
	__HorizontalVerifier__ NVARCHAR(MAX),
)
