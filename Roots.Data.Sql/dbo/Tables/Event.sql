CREATE TABLE [dbo].[Event] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [EventTypeId]     INT            NOT NULL,
    [PersonId]        INT            NOT NULL,
    [PlaceId]         INT            NOT NULL,
    [Date_Datetype]   INT            NOT NULL,
    [Date_DateFrom]   INT            NOT NULL,
    [Date_DateTo]     INT            NOT NULL,
    [Date_DatePhrase] NVARCHAR (255) NULL,
    [Date_DateString] VARCHAR (18)   NULL,
    [Date_DateLong]   BIGINT         NULL,
    [Date_IsValid]    BIT            NULL,
    [Description]     NVARCHAR (255) NULL,
    [CreatedDate]     DATETIME       NULL,
    [ModifiedDate]    DATETIME       NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventType] ([Id]),
    CONSTRAINT [FK_Event_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Event_Place] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Event_DateString_NN]
    ON [dbo].[Event]([Date_DateString] ASC);


GO
ALTER INDEX [IX_Event_DateString_NN]
    ON [dbo].[Event] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_Event_DateFrom_DateTo_NN]
    ON [dbo].[Event]([Date_DateFrom] ASC, [Date_DateTo] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_DateLong_NN]
    ON [dbo].[Event]([Date_DateLong] ASC)
    INCLUDE([EventTypeId], [PersonId], [PlaceId]);


GO
ALTER INDEX [IX_Event_DateLong_NN]
    ON [dbo].[Event] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_Event_DateTo_NN]
    ON [dbo].[Event]([Date_DateTo] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_DateFrom_NN]
    ON [dbo].[Event]([Date_DateFrom] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId_NN]
    ON [dbo].[Event]([EventTypeId] ASC)
    INCLUDE([Id], [Date_DateFrom], [Date_DateTo]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId_DateFrom_DateTo_NN]
    ON [dbo].[Event]([PersonId] ASC, [EventTypeId] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId_DateTo_NN]
    ON [dbo].[Event]([EventTypeId] ASC, [Date_DateTo] ASC)
    INCLUDE([Id]);


GO
ALTER INDEX [IX_Event_EventTypeId_DateTo_NN]
    ON [dbo].[Event] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_Event_PersonId_NN]
    ON [dbo].[Event]([PersonId] ASC)
    INCLUDE([Id]);

