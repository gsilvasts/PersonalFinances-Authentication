USE PersonalFinancesAuth
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [User] (
    [Id] UniqueIdentifier NOT NULL,
    [FirstName] VARCHAR(100) NOT NULL,
    [LastName] VARCHAR(100) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    [Role] VARCHAR(100) NOT NULL,
    [Active] BIT NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_User_Id] ON [User] ([Id]);
GO

CREATE INDEX [IX_User_Email] ON [User] ([Email]);
GO

COMMIT;
GO