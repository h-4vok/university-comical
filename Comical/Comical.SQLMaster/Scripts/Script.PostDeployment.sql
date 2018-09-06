INSERT DatabaseStatus (
	DatabaseName
)
SELECT
	DatabaseName = data.DatabaseName
FROM		(SELECT DatabaseName = 'comical' ) DATA
LEFT  JOIN	DatabaseStatus DS
		ON	DATA.DatabaseName = ds.DatabaseName

WHERE		ds.DatabaseName IS NULL
