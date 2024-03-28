For Email Notification please change SmtpSettings in appsetting.json
please create a Demo database and after that run below script on your SQL Server and change DefaultConnection in appsetings.json

USE [Demo]
GO

/****** Object: Table [dbo].[Customers] Script Date: 3/29/2024 3:41:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       VARCHAR (50)   NOT NULL,
    [LastName]        VARCHAR (50)   NOT NULL,
    [Password]        NVARCHAR (50)  NOT NULL,
    [ConfirmPassword] NVARCHAR (50)  NULL,
    [LoginUser]       VARCHAR (50)   NOT NULL,
    [Email]           NVARCHAR (50)  NOT NULL,
    [PhoneNumber]     NVARCHAR (50)  NOT NULL,
    [IsActive]        BIT            NOT NULL,
    [IsDeleted]       BIT            NOT NULL,
    [AddedDate]       DATETIME       NULL,
    [UpdatedDate]     DATETIME       NULL,
    [Token]           NVARCHAR (500) NULL
);




