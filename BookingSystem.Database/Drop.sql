IF DB_ID('BusTicketsBookingSystemDB') IS NULL
	PRINT 'Database not exist';
GO

USE BusTicketsBookingSystemDB;
GO

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
