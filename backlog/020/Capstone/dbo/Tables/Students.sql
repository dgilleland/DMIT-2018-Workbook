CREATE TABLE [dbo].[Students] (
    [StudentId] INT            IDENTITY (1, 1) NOT NULL,
    [SchoolId]  NVARCHAR (MAX) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED ([StudentId] ASC)
);

