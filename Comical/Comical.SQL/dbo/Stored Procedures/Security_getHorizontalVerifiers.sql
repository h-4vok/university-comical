CREATE PROCEDURE Security_getHorizontalVerifiers
	@TableName NVARCHAR(255)
AS
BEGIN

	DECLARE @query NVARCHAR(MAX)

	SET @query = 'SELECT __HorizontalVerifier__ FROM [' + @TableName + '] ORDER BY Id'
	EXEC(@query)

END