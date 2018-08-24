CREATE PROCEDURE UserRole_delete
	@userId INT
AS
BEGIN

	DELETE UserRole WHERE UserId = @userId

END