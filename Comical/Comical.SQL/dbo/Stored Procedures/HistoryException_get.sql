CREATE PROCEDURE HistoryException_get
AS
BEGIN

	SELECT
		he.Id,
		he.Section,
		he.ExceptionType,
		he.ExceptionSource,
		he.ExceptionMessage,
		he.ExceptionStackTrace,
		he.UserId,
		he.DateLogged
	
	FROM		HistoryException HE

END