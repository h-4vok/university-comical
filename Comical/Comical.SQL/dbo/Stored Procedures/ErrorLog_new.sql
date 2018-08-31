CREATE PROCEDURE ErrorLog_new
	@ExceptionMessage NVARCHAR(700),
	@ExceptionType NVARCHAR(300),
	@ExceptionSource NVARCHAR(300) = NULL,
	@ExceptionStackTrace NVARCHAR(MAX) = NULL,
	@UserId INT = NULL
AS
BEGIN

	INSERT ErrorLog (
		ExceptionMessage,
		ExceptionType,
		ExceptionSource,
		ExceptionStackTrace,
		LoggedBy
	)
	SELECT
		ExceptionMessage = @ExceptionMessage,
		ExceptionType = @ExceptionType,
		ExceptionSource = @ExceptionSource,
		ExceptionStackTrace = @ExceptionStackTrace,
		LoggedBy = @UserId

END