CREATE PROCEDURE User_new
	@Login NVARCHAR(70),
	@Password NVARCHAR(70)
AS
BEGIN

	INSERT [User] (
		Login,
		Password
	)
	SELECT
		Login = @Login,
		Password = @Password

	DECLARE @id INT = SCOPE_IDENTITY()

	SELECT @id

END