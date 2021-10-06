CREATE TABLE [dbo].[TeamAssignments] (
    [StudentId]  INT          NOT NULL,
    [ClientId]   INT          NOT NULL,
    [TeamNumber] NVARCHAR (1) NULL,
    CONSTRAINT [PK_dbo.TeamAssignments] PRIMARY KEY CLUSTERED ([StudentId] ASC, [ClientId] ASC),
    CONSTRAINT [FK_dbo.TeamAssignments_dbo.CapstoneClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[CapstoneClients] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.TeamAssignments_dbo.Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([StudentId]) ON DELETE CASCADE
);

