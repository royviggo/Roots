CREATE TABLE [dbo].[Person] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (255) NULL,
    [FatherName]   NVARCHAR (255) NULL,
    [Patronym]     NVARCHAR (255) NULL,
    [LastName]     NVARCHAR (255) NULL,
    [Gender]       INT            NULL,
    [BornYear]     INT            NULL,
    [DeathYear]    INT            NULL,
    [Status]       INT            NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedDate] DATETIME       NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);

