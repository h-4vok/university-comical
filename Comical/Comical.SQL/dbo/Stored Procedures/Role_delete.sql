CREATE PROCEDURE Role_delete
	@id INT
AS
BEGIN

	DELETE RolePermission WHERE RoleId = @id
	DELETE UserRole WHERE RoleId = @id
	DELETE [Role] WHERE Id = @id

END