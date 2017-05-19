IF DB_ID('BusTicketsBookingSystemDB') IS NULL
	BEGIN
		CREATE DATABASE BusTicketsBookingSystemDB
		COLLATE Cyrillic_General_CI_AS;
		PRINT 'Database created';
	END
ELSE
	PRINT 'Database already exists';
GO

USE BusTicketsBookingSystemDB;
GO

-- CLEANING UP

IF OBJECT_ID('[dbo].[Traffic]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Traffic];
GO

IF OBJECT_ID('[dbo].[Ticket]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Ticket];
GO

IF OBJECT_ID('[dbo].[Journey]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Journey];
GO

IF OBJECT_ID('[dbo].[Route]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Route];
GO

IF OBJECT_ID('[dbo].[Driver]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Driver];
GO

IF OBJECT_ID('[dbo].[Bus]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Bus];
GO

IF OBJECT_ID('[dbo].[RoutePoint]', 'U') IS NOT NULL
	DROP TABLE [dbo].[RoutePoint];
GO


IF OBJECT_ID('[dbo].[Passenger]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Passenger];
GO

IF OBJECT_ID('[dbo].[Administrator]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Administrator];
GO

-- CREATING TABLES

CREATE TABLE [dbo].[Driver]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100),
	[LastName] NVARCHAR(100),
	[Birthdate] DATE
);
GO

CREATE TABLE [dbo].[Bus]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Vin] NVARCHAR(20),
	[Model] NVARCHAR(100),
	[AutomobileNumber] NVARCHAR(10),
	[PassengersCount] INT
);
GO

CREATE TABLE [dbo].[RoutePoint]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Country] NVARCHAR(50),
	[State] NVARCHAR(50),
	[Region] NVARCHAR(50),
	[City] NVARCHAR(50)
);
GO

CREATE TABLE [dbo].[Route]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[DeparturePointId] INT NOT NULL,
	[ArrivalPointId] INT NOT NULL,
	[Length] FLOAT,
	CONSTRAINT [FK_DeparturePointId] FOREIGN KEY([DeparturePointId]) REFERENCES [dbo].[RoutePoint]([Id]),
	CONSTRAINT [FK_ArrivalPointId] FOREIGN KEY([ArrivalPointId]) REFERENCES [dbo].[RoutePoint]([Id])
);
GO

CREATE TABLE [dbo].[Journey]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[RouteId] INT NOT NULL,
	[BusId] INT NOT NULL,
	[DriverId] INT NOT NULL,
	[DepartureTime] DATETIME,
	[ArrivalTime] DATETIME,
	CONSTRAINT [FK_RouteId] FOREIGN KEY([RouteId]) REFERENCES [dbo].[Route]([Id]),
	CONSTRAINT [FK_BusId] FOREIGN KEY([BusId]) REFERENCES [dbo].[Bus]([Id]),
	CONSTRAINT [FK_DriverId] FOREIGN KEY([DriverId]) REFERENCES [dbo].[Driver]([Id])
);
GO

CREATE TABLE [dbo].[Passenger]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100),
	[LastName] NVARCHAR(100)
);
GO

CREATE TABLE [dbo].[Ticket]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[JourneyId] INT NOT NULL,
	[PassengerId] INT NOT NULL,
	[Price] MONEY,
	[Seat] INT,
	[PurchaseDateTime] DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	CONSTRAINT [FK_JourneyId] FOREIGN KEY([JourneyId]) REFERENCES [dbo].[Journey]([Id]),
	CONSTRAINT [FK_PassengerId] FOREIGN KEY([PassengerId]) REFERENCES [dbo].[Passenger]([Id])
);
GO

CREATE TABLE [dbo].[Traffic]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[JourneyId] INT NOT NULL,
	[TicketId] INT NOT NULL,
	CONSTRAINT [FK_JourneyTrafficId] FOREIGN KEY([JourneyId]) REFERENCES [dbo].[Journey]([Id]),
	CONSTRAINT [FK_TicketId] FOREIGN KEY([TicketId]) REFERENCES [dbo].[Ticket]([Id])
);
GO

CREATE TABLE [dbo].[Administrator]
(
	[Username] NVARCHAR(64) NOT NULL PRIMARY KEY,
	[Password] NVARCHAR(64) NOT NULL
);
GO
