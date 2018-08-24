CREATE PROCEDURE User_incrementRetry
	@id INT
AS
BEGIN

	UPDATE
		[User]
	SET
		Retries = Retries + 1
	WHERE
		Id = @id

	DECLARE @retries INT
	SELECT @retries = Retries FROM [User] WHERE Id = @id
	
	SELECT @retries

END