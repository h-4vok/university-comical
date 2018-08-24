CREATE PROCEDURE User_changeBlocked
	@id INT,
	@Blocked BIT
AS
BEGIN	

	UPDATE
		[User]
	SET
		Blocked = @Blocked
	WHERE
		Id = @id

	IF (@Blocked = 0)
	BEGIN
		
		UPDATE
			[User]
		SET
			Retries = 0
		WHERE
			Id = @id

	END

END