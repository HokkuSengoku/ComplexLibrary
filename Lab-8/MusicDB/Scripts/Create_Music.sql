CREATE DATABASE [Music]	
GO

USE [Music]
GO

CREATE TABLE [dbo].[albums] 
(
	[albumId] INT IDENTITY (1, 1) PRIMARY KEY, 
	[date] DATE,
	[title] NVARCHAR(255) NOT NULL 
);
GO

CREATE TABLE [dbo].[songs] 
(
	[songId] INT IDENTITY (1, 1) PRIMARY KEY, 
	[albumId] INT NOT NULL REFERENCES [dbo].[albums] ON DELETE CASCADE,
	[title] NVARCHAR(255) NOT NULL,
	[duration] TIME NOT NULL
);
GO

CREATE PROCEDURE [dbo].[uspAddAlbum]
	@Title NVARCHAR(255),
	@Date DATE
AS
	INSERT INTO [albums] ([title], [date]) VALUES (@Title, @Date);
	RETURN (@@IDENTITY);
GO

CREATE PROCEDURE [dbo].[uspAddSong]
	@Title NVARCHAR(255),
	@AlbumId int,
	@Duration TIME
AS
	INSERT INTO [songs] ([title], [albumId], [duration]) VALUES (@Title, @AlbumId, @Duration);
	RETURN (@@IDENTITY);
GO
