-- Sample API Key Generation
-- Date: 2025-11-18
-- Description: Creates a sample API key for testing purposes

USE HomeAppsV3;
GO

-- Sample API Key: "test-api-key-12345"
-- SHA-256 Hash of the above key
DECLARE @SampleKeyHash NVARCHAR(500) = 'i9WqXOPxmUB8TQgNLxcLQHZ5FJ6xqLJvYLhZ3mZR8Ps=';
DECLARE @KeyName NVARCHAR(100) = 'Test API Key';
DECLARE @Description NVARCHAR(1000) = 'Sample API key for testing. Use key: test-api-key-12345';

-- Insert sample API key if it doesn't exist
IF NOT EXISTS (SELECT 1 FROM [Kids].[ApiKeys] WHERE [KeyName] = @KeyName AND [IsDeleted] = 0)
BEGIN
    INSERT INTO [Kids].[ApiKeys]
    (
        [KeyName],
        [KeyValue],
        [Description],
        [IsActive],
        [ExpiresAt],
        [IsDeleted],
        [CreatedAt],
        [ModifiedAt]
    )
    VALUES
    (
        @KeyName,
        @SampleKeyHash,
        @Description,
        1,  -- IsActive
        DATEADD(YEAR, 1, GETUTCDATE()),  -- Expires in 1 year
        0,  -- Not deleted
        GETUTCDATE(),
        GETUTCDATE()
    );

    PRINT 'Sample API key created successfully!';
    PRINT '';
    PRINT '=================================================';
    PRINT 'API Key: test-api-key-12345';
    PRINT 'Header: X-API-Key: test-api-key-12345';
    PRINT 'Expires: ' + CONVERT(VARCHAR, DATEADD(YEAR, 1, GETUTCDATE()), 120);
    PRINT '=================================================';
    PRINT '';
    PRINT 'IMPORTANT: This is a test key. Generate a new secure key for production use!';
END
ELSE
BEGIN
    PRINT 'Sample API key already exists.';
END
GO
