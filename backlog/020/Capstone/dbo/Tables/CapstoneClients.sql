CREATE TABLE [dbo].[CapstoneClients] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName] NVARCHAR (MAX) NOT NULL,
    [Slogan]      NVARCHAR (MAX) NULL,
    [ContactName] NVARCHAR (MAX) NOT NULL,
    [Confirmed]   BIT            NOT NULL,
    CONSTRAINT [PK_dbo.CapstoneClients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

