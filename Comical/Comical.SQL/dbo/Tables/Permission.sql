CREATE TABLE [dbo].[Permission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Code NVARCHAR(70),
	__HorizontalVerifier__ NVARCHAR(MAX),
)
