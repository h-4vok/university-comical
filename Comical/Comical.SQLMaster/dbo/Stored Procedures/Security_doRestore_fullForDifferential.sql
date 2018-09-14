CREATE PROCEDURE Security_doRestore_fullForDifferential
	@backup_fileName_p NVARCHAR(500),
	@DBName NVARCHAR(50)
AS
BEGIN

	EXEC Database_killAll @DBName  
 
	RESTORE DATABASE @DBName FROM DISK = @backup_FileName_p WITH RECOVERY

END
