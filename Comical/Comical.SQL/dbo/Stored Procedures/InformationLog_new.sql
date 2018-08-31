CREATE PROCEDURE InformationLog_new
	@Message NVARCHAR(700),
	@UserId INT = NULL
AS
BEGIN

	INSERT InformationLog (
		Message,
		LoggedBy
	)
	SELECT
		Message = @Message,
		LoggedBy = @UserId

END