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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230107021239_Initial')
BEGIN
    CREATE TABLE [Posts] (
        [id] int NOT NULL IDENTITY,
        [title] nvarchar(max) NOT NULL,
        [content] nvarchar(max) NOT NULL,
        [author] nvarchar(max) NOT NULL,
        [Likes] int NOT NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230107021239_Initial')
BEGIN
    CREATE TABLE [Comments] (
        [id] int NOT NULL IDENTITY,
        [PostId] int NOT NULL,
        [content] nvarchar(max) NOT NULL,
        [author] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230107021239_Initial')
BEGIN
    CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230107021239_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230107021239_Initial', N'7.0.1');
END;
GO

COMMIT;
GO

