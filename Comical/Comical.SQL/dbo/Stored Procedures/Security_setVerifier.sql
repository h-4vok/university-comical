CREATE PROCEDURE Security_setVerifier
	@table NVARCHAR(255),
	@verifier NVARCHAR(MAX),
	@where NVARCHAR(MAX)
AS
BEGIN

	DECLARE @sql NVARCHAR(MAX)

	SET @sql = CONCAT('UPDATE [', @table, '] SET __HorizontalVerifier__ = ''', @verifier, ''' WHERE ', @where)

	EXEC(@sql)

END