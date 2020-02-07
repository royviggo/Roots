CREATE TABLE [dbo].[Place] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255) NOT NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedDate] DATETIME       NULL,
    CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED ([Id] ASC)
);

