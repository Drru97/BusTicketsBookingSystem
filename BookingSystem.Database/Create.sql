IF DB_ID('BusTicketsBookingSystemDB') IS NULL
	BEGIN
		CREATE DATABASE BusTicketsBookingSystemDB;
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

-- CREATING TABLES

CREATE TABLE [dbo].[Driver]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100),
	[LastName] NVARCHAR(100),
	[Birthdate] DATE
);
GO

CREATE TABLE [dbo].[Bus]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[VIN] NVARCHAR(20),
	[Model] NVARCHAR(100),
	[AutomobileNumber] NVARCHAR(10),
	[PassengersCount] INT
);
GO

CREATE TABLE [dbo].[RoutePoint]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[Country] NVARCHAR(50),
	[State] NVARCHAR(50),
	[Region] NVARCHAR(50),
	[City] NVARCHAR(50)
);
GO

CREATE TABLE [dbo].[Route]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[DeparturePointID] INT NOT NULL,
	[ArrivalPointID] INT NOT NULL,
	[Length] FLOAT,
	CONSTRAINT [FK_DeparturePointID] FOREIGN KEY([DeparturePointID]) REFERENCES [dbo].[RoutePoint]([ID]),
	CONSTRAINT [FK_ArrivalPointID] FOREIGN KEY([ArrivalPointID]) REFERENCES [dbo].[RoutePoint]([ID])
);
GO

CREATE TABLE [dbo].[Journey]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[RouteID] INT NOT NULL,
	[BusID] INT NOT NULL,
	[DriverID] INT NOT NULL,
	[DepartureTime] DATETIME,
	[ArrivalTime] DATETIME,
	CONSTRAINT [FK_RouteID] FOREIGN KEY([RouteID]) REFERENCES [dbo].[Route]([ID]),
	CONSTRAINT [FK_BusID] FOREIGN KEY([BusID]) REFERENCES [dbo].[Bus]([ID]),
	CONSTRAINT [FK_DriverID] FOREIGN KEY([DriverID]) REFERENCES [dbo].[Driver]([ID])
);
GO

CREATE TABLE [dbo].[Passenger]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100),
	[LastName] NVARCHAR(100)
);
GO

CREATE TABLE [dbo].[Ticket]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[JourneyID] INT NOT NULL,
	[PassengerID] INT NOT NULL,
	[Price] MONEY,
	[Seat] INT,
	[PurchaseDateTime] DATETIME NOT NULL DEFAULT(SYSDATETIME()),
	CONSTRAINT [FK_JourneyID] FOREIGN KEY([JourneyID]) REFERENCES [dbo].[Journey]([ID]),
	CONSTRAINT [FK_PassengerID] FOREIGN KEY([PassengerID]) REFERENCES [dbo].[Passenger]([ID])
);
GO

CREATE TABLE [dbo].[Traffic]
(
	[ID] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[JourneyID] INT NOT NULL,
	[TicketID] INT NOT NULL,
	CONSTRAINT [FK_JourneyTrafficID] FOREIGN KEY([JourneyID]) REFERENCES [dbo].[Journey]([ID]),
	CONSTRAINT [FK_TicketID] FOREIGN KEY([TicketID]) REFERENCES [dbo].[Ticket]([ID])
);
GO