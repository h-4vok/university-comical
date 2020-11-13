CREATE PROCEDURE Security_getAllRecords
	@table NVARCHAR(255)
AS
BEGIN

	DECLARE @sql NVARCHAR(MAX)

	SET @sql = CONCAT('SELECT * FROM [', @table, ']')

	EXEC(@sql)

END