CREATE PROCEDURE Security_doRestore
	@backup_fileName_p NVARCHAR(500),
	@DBName NVARCHAR(50)
AS
BEGIN
	
	-- Kill any open connections to this database
	EXEC Database_killAll @DBName    

	-- Restore database using the given backup filename
	RESTORE DATABASE @DBName FROM DISK = @backup_FileName_p WITH RECOVERY, REPLACE  

END