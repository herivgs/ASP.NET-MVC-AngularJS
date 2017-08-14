CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NCHAR(50) NULL, 
    [Email] NCHAR(50) NULL, 
    [Password] NCHAR(50) NULL, 
    [ConfirmationPassword] NCHAR(50) NULL
)
