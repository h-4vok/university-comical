CREATE PROCEDURE Role_update
	@code NVARCHAR(70),
	@description NVARCHAR(70),
	@enabled BIT,
	@id INT
AS
BEGIN

	UPDATE [Role] SET
		Code = @code,
		Description = @description,
		Enabled = @enabled
	WHERE		Id = @id

END