CREATE TABLE [dbo].[Child]
(
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FamilyId]     INT            NOT NULL,
    [PersonId]     INT            NOT NULL, 
    [CreatedDate]  DATETIME       NULL, 
    CONSTRAINT [PK_Child] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Child_Family] FOREIGN KEY ([FamilyId]) REFERENCES [dbo].[Family] ([Id]),
    CONSTRAINT [FK_Child_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
)
