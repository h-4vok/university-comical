CREATE PROCEDURE User_getByLogin
	@Login NVARCHAR(70)
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

	WHERE		Login = @Login

END