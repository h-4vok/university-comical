CREATE PROCEDURE HistoryEvent_get
AS
BEGIN

	SELECT
		he.Id,
		he.Section,
		he.Message,
		he.UserId,
		he.DateLogged

	FROM		HistoryEvent HE WITH (NOLOCK)

END