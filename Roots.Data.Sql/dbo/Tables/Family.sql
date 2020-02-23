CREATE TABLE [dbo].[Family]
(
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedDate] DATETIME       NULL, 
    CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED ([Id] ASC),
)
