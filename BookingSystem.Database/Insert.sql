﻿USE BusTicketsBookingSystemDB;
GO

SET IDENTITY_INSERT [dbo].[Driver] ON;
INSERT INTO [dbo].[Driver] ([Id], [FirstName],[LastName], [Birthdate]) VALUES
	(1, 'Oleg', 'Volyanchuk', '1974-08-23'), 
	(2, 'Petro', 'Vasylyshyn', '1982-11-16'), 
	(3, 'Ihor', 'Sidorovich', '1969-02-04'), 
	(4, 'Stepan', 'Dmytriv', '1986-07-05');
SET IDENTITY_INSERT [dbo].[Driver] OFF;
GO

SET IDENTITY_INSERT [dbo].[Bus] ON;
INSERT INTO [dbo].[Bus] ([Id], [Model], [Vin], [AutomobileNumber], [PassengersCount]) VALUES
	(1, 'БАЗ-А079 Еталон', 'JN1AZ4FH2FM430020', 'ВС2826АК', 22),
	(2, 'MAN 11.190', '4F2CY0GG0AKM01112', 'АА6281ВМ', 38),
	(3, 'Neoplan 116', 'WP1AC29P58L342170', 'АК6352ВЕ', 53),
	(4, 'DAF SB', '3D7KR28D08G206868', 'АС7654ВВ', 56);
SET IDENTITY_INSERT [dbo].[Bus] OFF;
GO

SET IDENTITY_INSERT [dbo].[RoutePoint] ON;
INSERT INTO [dbo].[RoutePoint] ([Id], [Country], [State], [Region], [City]) VALUES
	(1, 'Україна', 'Львівська', NULL, 'м. Львів'),
	(2, 'Україна', 'Львівська', 'Жовківський', 'смт. Куликів'),
	(3, 'Україна', 'Львівська', 'Сколівський', 'с. Гребенів'),
	(4, 'Україна', NULL, NULL, 'м. Київ'),
	(5, 'Україна', 'Харківський', NULL, 'м. Харків');
SET IDENTITY_INSERT [dbo].[RoutePoint] OFF;
GO

SET IDENTITY_INSERT [dbo].[Route] ON;
INSERT INTO [dbo].[Route] ([Id], [DeparturePointId], [ArrivalPointId], [Length]) VALUES
	(1, 1, 4, 536),
	(2, 1, 2, 16),
	(3, 4, 5, 390),
	(4, 3, 1, 118);
SET IDENTITY_INSERT [dbo].[Route] OFF;
GO

SET IDENTITY_INSERT [dbo].[Journey] ON;
INSERT INTO [dbo].[Journey] ([Id], [RouteId], [BusId], [DriverId], [DepartureTime], [ArrivalTime]) VALUES
	(1, 1, 3, 2, '2017-05-20 21:30', '2017-05-21 06:30'),
	(2, 2, 1, 4, '2017-05-25 12:40', '2017-05-25 13:30'),
	(3, 3, 4, 4, '2017-05-22 08:00', '2017-05-22 14:10'),
	(4, 4, 1, 3, '2017-05-21 10:45', '2017-05-21 12:55');
SET IDENTITY_INSERT [dbo].[Journey] OFF;
GO

INSERT INTO [dbo].[Administrator] ([Username], [Password]) VALUES
	('admin', '7c87541fd3f3ef5016e12d411900c87a6046a8e8'),
	('administrator', 'ffa50dd8e728e95dac10aba8a0022ecebace52b9'),
	('test', '87f8ed9157125ffc4da9e06a7b8011ad80a53fe1');
GO
