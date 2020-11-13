CREATE PROCEDURE HistoryException_new
	@Section NVARCHAR(70),
	@ExceptionType NVARCHAR(300),
	@ExceptionSource NVARCHAR(300) = NULL,
	@ExceptionMessage NVARCHAR(700),
	@ExceptionStackTrace NVARCHAR(MAX) = NULL,
	@UserId INT = NULL
AS
BEGIN

	INSERT HistoryException (
		Section,
		ExceptionType,
		ExceptionSource,
		ExceptionMessage,
		ExceptionStackTrace,
		UserId,
		DateLogged
	)
	SELECT
		Section = @Section,
		ExceptionType = @ExceptionType,
		ExceptionSource = @ExceptionSource,
		ExceptionMessage = @ExceptionMessage,
		ExceptionStackTrace = @ExceptionStackTrace,
		UserId = @UserId,
		DateLogged = GETDATE()

	DECLARE @id INT = SCOPE_IDENTITY()
	SELECT @id

END