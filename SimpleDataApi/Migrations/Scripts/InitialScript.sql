IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [CodeValues] (
    [Id] int NOT NULL IDENTITY,
    [Code] int NOT NULL,
    [Value] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_CodeValues] PRIMARY KEY CLUSTERED ([Id])
);

CREATE NONCLUSTERED INDEX [IX_CodeValues_Code_Value] ON [CodeValues] ([Code], [Value]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250327172249_InitialCreate', N'9.0.3');

COMMIT;
GO

