CREATE PROCEDURE VerticalVerifier_update
	@TableName NVARCHAR(255),
	@VerticalVerifier NVARCHAR(MAX)
AS
BEGIN

	IF (EXISTS(SELECT TOP 1 1 FROM VerticalVerifier WHERE TableName = @TableName))
	BEGIN

		UPDATE
			VerticalVerifier
		SET
			VerticalVerifier = @VerticalVerifier
		WHERE		TableName = @TableName

	END
	ELSE
	BEGIN

		INSERT VerticalVerifier ( TableName, VerticalVerifier )
		VALUES ( @TableName, @VerticalVerifier )

	END

END