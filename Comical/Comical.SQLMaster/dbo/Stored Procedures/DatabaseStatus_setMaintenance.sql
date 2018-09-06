CREATE PROCEDURE DatabaseStatus_setMaintenance
	@DatabaseName NVARCHAR(255),
	@UnderMaintenance BIT
AS
BEGIN

	UPDATE 
		DatabaseStatus
	SET
		UnderMaintenance = @UnderMaintenance
	WHERE		DatabaseName = @DatabaseName

END