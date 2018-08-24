CREATE PROCEDURE User_update
	@id INT,
	@Login NVARCHAR(70),
	@Password NVARCHAR(70),
	@Enabled BIT,
	@Blocked BIT
AS
BEGIN

	UPDATE
		[User]
	SET
		Login = @Login,
		Password = @Password,
		Enabled = @Enabled,
		Blocked = @Blocked
	WHERE
		Id = @id

END