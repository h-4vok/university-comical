CREATE PROCEDURE Permission_getByUser
	@userId INT
AS
BEGIN

	SELECT DISTINCT p.Code
	
	FROM		UserRole UR

	INNER JOIN	Role R
			ON	ur.RoleId = r.Id

	INNER JOIN	RolePermission RP
			ON	r.Id = rp.RoleId

	INNER JOIN	Permission P
			ON	rp.PermissionId = p.Id

	WHERE		ur.UserId = @userId

END