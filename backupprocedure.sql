CREATE PROCEDURE [dbo].[HEVAFUD]
    @location VARCHAR(250) = NULL
AS
BEGIN
    DECLARE @path VARCHAR(256)  -- Path for backup files
    DECLARE @name VARCHAR(256)  -- Database name
    DECLARE @date VARCHAR(20)   -- Used for filename
    DECLARE @fileName VARCHAR(256)  -- Backup file name

  SET @path = @location + ''  -- Define the backup folder

    SET @name = 'hevafud'  -- Change this to your database name

    -- Specify filename format
    SELECT @date = CONVERT(VARCHAR(20), GETDATE(), 112)

    BEGIN
        SET @fileName = @path + @name + '_' + @date + '.BAK'

        -- Perform database backup
        BACKUP DATABASE @name TO DISK = @fileName
    END
END
