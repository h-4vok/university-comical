CREATE PROCEDURE Role_newPermission
	@roleId INT,
	@permissionId INT
AS
BEGIN

	INSERT RolePermission (
		RoleId,
		PermissionId
	)
	SELECT
		RoleId = @roleId,
		PermissionId = @permissionId

	SELECT SCOPE_IDENTITY()

END