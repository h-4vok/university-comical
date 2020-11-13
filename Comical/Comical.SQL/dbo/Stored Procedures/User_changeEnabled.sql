CREATE PROCEDURE User_changeEnabled
	@id INT,
	@Enabled BIT
AS
BEGIN

	UPDATE
		[User]
	SET
		Enabled = @Enabled
	WHERE
		Id = @id

END