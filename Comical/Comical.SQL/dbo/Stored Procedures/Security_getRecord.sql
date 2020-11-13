CREATE PROCEDURE Security_getRecord
	@table NVARCHAR(255),
	@where NVARCHAR(MAX)
AS
BEGIN

	DECLARE @sql NVARCHAR(MAX)

	SET @sql = CONCAT('SELECT * FROM [', @table, '] WITH (NOLOCK) WHERE ', @where)

	EXEC(@sql)

END