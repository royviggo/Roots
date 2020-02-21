CREATE TABLE [dbo].[Family]
(
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FatherId]     INT            NULL, 
    [MotherId]     INT            NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedDate] DATETIME       NULL, 
    CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Family_Father] FOREIGN KEY ([FatherId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Family_Mother] FOREIGN KEY ([MotherId]) REFERENCES [dbo].[Person] ([Id]),
)
