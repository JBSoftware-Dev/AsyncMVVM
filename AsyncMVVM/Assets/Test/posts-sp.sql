CREATE PROCEDURE sp_TestGetPosts
AS

DECLARE @Posts TABLE(Title VARCHAR(250), Content VARCHAR(250) , PostDate DATETIME)
DECLARE @i INT 
SET @i = 1

WHILE ( @i <= 20)
BEGIN
    INSERT INTO @Posts VALUES (
		'Post ' + CONVERT(VARCHAR, @i),
		'The quick brown fox jumps over the lazy dog',
		GETDATE()
	)
    SET @i = @i + 1
END

SELECT * FROM @Posts