CREATE PROCEDURE Security_doRestoreIncremental
	@backup_fileName_p VARCHAR(500),
	@DBName VARCHAR(50)
AS
BEGIN

	-- Kill all live connections to this database
	EXEC Database_killAll @DBName  

	-- Perform incremental recovery from the given file
	RESTORE DATABASE @DBName FROM DISK = @backup_fileName_p WITH NORECOVERY

END