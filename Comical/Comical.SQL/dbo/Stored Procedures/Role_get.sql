CREATE PROCEDURE Role_get
AS
BEGIN

	SELECT
		r.Id,
		r.Code,
		r.Description,
		r.Enabled

	FROM		Role R

END