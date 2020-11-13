CREATE PROCEDURE DatabaseStatus_setHasChecksumError
	@DatabaseName NVARCHAR(255),
	@HasChecksumError BIT
AS
BEGIN

	UPDATE 
		DatabaseStatus
	SET
		HasChecksumError = @HasChecksumError
	WHERE		DatabaseName = @DatabaseName

END