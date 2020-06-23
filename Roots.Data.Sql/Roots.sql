CREATE LOGIN [roots] WITH PASSWORD = 'Password123!', DEFAULT_DATABASE=[roots]
GO

CREATE USER [roots] FOR LOGIN [roots] WITH DEFAULT_SCHEMA = dbo
GO

GRANT CONNECT TO [roots]
GO

ALTER ROLE [db_datareader] ADD MEMBER [roots]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [roots]
GO