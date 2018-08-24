CREATE PROCEDURE Permission_isGrantedToUser
	@userId INT,
	@permissionCode NVARCHAR(70)
AS
BEGIN

	DECLARE @permissionId INT
	SELECT 
		@permissionId = p.Id
	FROM		Permission P
	WHERE		P.Code = @permissionCode



END