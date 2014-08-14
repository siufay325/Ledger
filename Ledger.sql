CREATE DATABASE [Remittance]
GO

USE [Remittance]

CREATE TABLE [dbo].[User](
	[UserID] [smallint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TeamID] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL UNIQUE
)

INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '001')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '002')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '003')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '005')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '006')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '007')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '008')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '009')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '010')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '013')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '015')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '016')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '018')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '019')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '020')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '021')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '023')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '026')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '027')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '028')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '029')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '030')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '031')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '032')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '033')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '035')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '036')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '037')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '038')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (2, '039')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '101')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '105')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '108')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '109')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '110')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '111')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '117')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '120')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '123')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '125')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '126')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '127')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '128')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '129')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '130')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '131')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '132')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '133')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '135')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '136')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '137')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '138')
INSERT INTO [dbo].[User] ([TeamID], [Name]) VALUES (1, '139')

CREATE TABLE [dbo].[Ledger](
	[LedgerID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserID] [smallint] NOT NULL FOREIGN KEY REFERENCES [User]([UserID]),
	[Date] [date] NOT NULL,
	[ReportAmount] [money] NOT NULL
)

CREATE TABLE [dbo].[CurrencyDenomination](
	[CurrencyDenominationID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[LedgerID] [int] NOT NULL FOREIGN KEY REFERENCES [Ledger]([LedgerID]),
	[CurrencyDenominationTypeID] [tinyint] NOT NULL,
	[Quantity10000] [smallint] NOT NULL,
	[Quantity1000] [smallint] NOT NULL,
	[Quantity100] [smallint] NOT NULL,
	[Quantity50] [smallint] NOT NULL,
	[Quantity10] [smallint] NOT NULL,
	[Quantity5] [smallint] NOT NULL,
	[Quantity2] [smallint] NOT NULL,
	[CoinAmount] [smallmoney] NOT NULL
)

CREATE TABLE [dbo].[Pay](
	[PayID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[LedgerID] [int] NOT NULL FOREIGN KEY REFERENCES [Ledger]([LedgerID]),
	[PayTypeID] [tinyint] NOT NULL,
	[Amount] [money] NOT NULL,
	[Remark] [nvarchar](50) NULL,
)

CREATE TABLE [dbo].[Cheque](
	[ChequeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[LedgerID] [int] NOT NULL FOREIGN KEY REFERENCES [Ledger]([LedgerID]),
	[Number] [nvarchar](50) NOT NULL,
	[Bank] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL
)

CREATE TABLE [dbo].[EC](
	[ECID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[LedgerID] [int] NOT NULL FOREIGN KEY REFERENCES [Ledger]([LedgerID]),
	[PurchaseDate] [date] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ICNumber] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](50) NOT NULL,
	[IDType] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](50) NOT NULL,
	[ReceivedDate] [nvarchar](50) NOT NULL,
	[Currency] [nvarchar](50) NOT NULL,
	[AmountForeignCurrency] [money] NOT NULL,
	[ExchangeRate] [decimal](12,4) NOT NULL,
)