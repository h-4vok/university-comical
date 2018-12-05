CREATE PROCEDURE Role_getById
	@id INT
AS
BEGIN

	SELECT
		r.Id,
		r.Code,
		r.Description,
		r.Enabled
	FROM		[Role] R
	WHERE		r.Id = @id

END