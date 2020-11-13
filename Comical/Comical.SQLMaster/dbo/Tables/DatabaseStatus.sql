CREATE TABLE [dbo].[DatabaseStatus]
(
	DatabaseName NVARCHAR(255) NOT NULL,
	UnderMaintenance BIT NOT NULL DEFAULT 0,
	HasChecksumError BIT NOT NULL DEFAULT 0
)
