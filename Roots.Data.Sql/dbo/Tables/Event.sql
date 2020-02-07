CREATE TABLE [dbo].[Event] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [EventTypeId]     INT            NOT NULL,
    [PersonId]        INT            NOT NULL,
    [PlaceId]         INT            NOT NULL,
    [EventDate]       VARCHAR (18)   NULL,
    [Description]     NVARCHAR (255) NULL,
    [CreatedDate]     DATETIME       NULL,
    [ModifiedDate]    DATETIME       NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventType] ([Id]),
    CONSTRAINT [FK_Event_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Event_Place] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventDate_NN]
    ON [dbo].[Event]([EventDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId_NN]
    ON [dbo].[Event]([EventTypeId] ASC)
    INCLUDE([Id], [EventDate]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId_EventDate_NN]
    ON [dbo].[Event]([EventTypeId] ASC, [EventDate] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_Event_PersonId_NN]
    ON [dbo].[Event]([PersonId] ASC)
    INCLUDE([Id]);

