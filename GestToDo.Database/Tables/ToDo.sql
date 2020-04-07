CREATE TABLE [dbo].[ToDo]
(
	Id INT NOT NULL Identity, 
	Title NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(255) NOT NULL,
	Done bit NOT NULL
		CONSTRAINT DF_ToDo_Done DEFAULT (0),
	ValidationDate DATETIME2(7) NULL,
	UserId int NOT NULL,
    CONSTRAINT [PK_ToDo] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ToDo_User] FOREIGN KEY (UserId) references [User](Id)
)

GO

CREATE TRIGGER [dbo].[TR_AfterUpdateToDo]
    ON [dbo].[ToDo]
    FOR UPDATE
    AS
    BEGIN
		set NoCount on;

        if exists (Select * from deleted)
		Begin
			Update ToDo 
			Set ValidationDate = SYSDATETIME()
			where Id in (Select d.Id 
						 from deleted d
						 join inserted i on d.Id = i.Id
						 where d.Done = 0 and i.Done = 1); 

			Update ToDo 
			Set ValidationDate = NULL
			where Id in (Select d.Id 
						 from deleted d
						 join inserted i on d.Id = i.Id
						 where d.Done = 1 and i.Done = 0); 
		End
    END