CREATE PROCEDURE Role_deletePermissions
	@roleId INT
AS
BEGIN

	DELETE RolePermission WHERE RoleId = @roleId

END