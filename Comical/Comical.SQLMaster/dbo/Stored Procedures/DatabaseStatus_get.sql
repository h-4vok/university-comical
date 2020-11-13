CREATE PROCEDURE DatabaseStatus_get
	@DatabaseName NVARCHAR(255)
AS
BEGIN

	SELECT
		DatabaseName,
		UnderMaintenance,
		HasChecksumError
	FROM		DatabaseStatus
	WHERE		DatabaseName = @DatabaseName

END
