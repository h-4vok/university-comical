CREATE PROCEDURE User_getById
	@id INT
AS
BEGIN
	
	SELECT
		u.Id,
		u.Login,
		u.Password,
		u.Enabled,
		u.Blocked,
		u.Retries

	FROM		[User] U

	WHERE		Id = @id

END