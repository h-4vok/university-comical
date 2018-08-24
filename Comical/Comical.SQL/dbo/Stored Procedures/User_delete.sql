CREATE PROCEDURE User_delete
	@id INT
AS
BEGIN

	DELETE UserRole WHERE UserId = @id
	DELETE [User] WHERE Id = @id

END