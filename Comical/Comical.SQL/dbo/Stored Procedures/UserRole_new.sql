CREATE PROCEDURE UserRole_new
	@userId INT,
	@roleId INT
AS
BEGIN

	INSERT UserRole (
		UserId,
		RoleId
	)
	SELECT
		UserId = @userId,
		RoleId = @roleId

END