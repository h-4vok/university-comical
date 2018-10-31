CREATE PROCEDURE HistoryEvent_new
	@Section NVARCHAR(70),
	@Message NVARCHAR(300),
	@UserId INT = NULL
AS
BEGIN

	INSERT HistoryEvent (
		Section,
		Message,
		UserId,
		DateLogged
	)
	SELECT
		Section = @Section,
		Message = @Message,
		UserId = @UserId,
		DateLogged = GETDATE()

	DECLARE @id INT = SCOPE_IDENTITY()
	SELECT @id

END