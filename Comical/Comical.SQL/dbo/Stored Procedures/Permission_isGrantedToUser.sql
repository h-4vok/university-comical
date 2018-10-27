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

	DECLARE @found INT = NULL

	SELECT TOP 1 
		@found = rp.Id
	
	FROM		UserRole UR

	INNER JOIN	Role R
			ON	ur.RoleId = r.Id

	INNER JOIN	RolePermission RP
			ON	r.Id = rp.RoleId

	WHERE		rp.PermissionId = @permissionId
	AND			ur.UserId = @userId

	SELECT CONVERT(BIT, CASE WHEN @found IS NOT NULL THEN 1 ELSE 0 END)

END