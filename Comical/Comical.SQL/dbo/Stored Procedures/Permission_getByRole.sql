CREATE PROCEDURE Permission_getByRole
	@roleId INT
AS
BEGIN

	SELECT 
		p.Id,
		p.Code
	
	FROM		RolePermission RP

	INNER JOIN	Permission P
			ON	rp.PermissionId = p.Id

	WHERE		rp.RoleId = @roleId

END