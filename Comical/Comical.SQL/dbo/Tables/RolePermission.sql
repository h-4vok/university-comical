CREATE TABLE [dbo].[RolePermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	RoleId INT NOT NULL,
	PermissionId INT NOT NULL,
	__HorizontalVerifier__ NVARCHAR(MAX),

	FOREIGN KEY (RoleId) REFERENCES Role,
	FOREIGN KEY (PermissionId) REFERENCES Permission
)
