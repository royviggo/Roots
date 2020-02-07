CREATE TABLE [dbo].[EventType] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [IsFamilyEvent]  BIT            NOT NULL,
    [Name]           NVARCHAR (255) NOT NULL,
    [GedcomTag]      NVARCHAR (255) NULL,
    [CreatedDate]    DATETIME       NULL,
    [ModifiedDate]   DATETIME       NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

