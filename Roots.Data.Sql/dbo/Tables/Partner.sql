CREATE TABLE [dbo].[Partner] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [FamilyId]     INT      NOT NULL,
    [PersonId]     INT      NOT NULL,
    [PartnerRoleId] INT      NOT NULL,
    [CreatedDate]  DATETIME NULL,
    [ModifiedDate] DATETIME NULL,
    CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Partner_Family] FOREIGN KEY ([FamilyId]) REFERENCES [dbo].[Family] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Partner_PartnerRole] FOREIGN KEY ([PartnerRoleId]) REFERENCES [dbo].[PartnerRole] ([Id]),
    CONSTRAINT [FK_Partner_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);



