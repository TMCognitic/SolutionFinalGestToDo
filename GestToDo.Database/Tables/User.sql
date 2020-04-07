CREATE TABLE [dbo].[User]
(
	Id INT NOT NULL IDENTITY,
	LastName nvarchar(75) NOT NULL,
	FirstName nvarchar(75) NOT NULL,
	Email nvarchar(384) NOT NULL,
	Passwd nvarchar(20) NOT NULL, 
	Active bit NOT NULL 
		CONSTRAINT DF_User_Active Default (1),
    CONSTRAINT [PK_Utilisateur] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Utilisateur_Email] Unique (Email),
)

GO

CREATE TRIGGER [dbo].[TR_InstedOfDeleteUser]
    ON [dbo].[User]
    INSTEAD OF DELETE
    AS
    BEGIN
        SET NoCount ON
		UPDATE [User] Set Active = 0 where Id in (Select Id from deleted);
    END