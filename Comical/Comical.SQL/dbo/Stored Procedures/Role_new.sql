CREATE PROCEDURE Role_new
	@code NVARCHAR(70),
	@description NVARCHAR(70),
	@enabled BIT
AS
BEGIN

	INSERT [Role] (
		Code,
		Description,
		Enabled
	)
	SELECT
		Code = @code,
		Description = @description,
		Enabled = @enabled

	SELECT SCOPE_IDENTITY()

END