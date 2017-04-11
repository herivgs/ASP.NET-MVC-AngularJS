/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [Coqueta]
GO

INSERT INTO [dbo].[Users] ([Username], [Email] ,[Password] ,[ConfirmationPassword])
     VALUES('herivgs', 'herivgs@hotmail.com' ,'Password123' ,'Password123')

INSERT INTO [dbo].[Users] ([Username] ,[Email] ,[Password] ,[ConfirmationPassword])
     VALUES ('m.leticia' ,'m.leticia@latinchat.com' ,'JaJeJoJu' ,'JaJeJoJu')

GO