CREATE PROCEDURE User_get
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

END