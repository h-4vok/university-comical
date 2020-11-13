CREATE PROCEDURE Database_doBackup_Comical_full
	@filepath NVARCHAR(255)
AS
BEGIN

	EXEC Database_killAll 'Comical'

	BACKUP DATABASE Comical
	TO DISK = @filepath
	WITH FORMAT,  
      MEDIANAME = 'C_SQLServerBackups',  
      NAME = 'Full Backup of Comical';  

END
