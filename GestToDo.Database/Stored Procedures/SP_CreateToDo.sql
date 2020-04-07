CREATE PROCEDURE [ToDoApp].[SP_CreateToDo]
	@Title nvarchar(128),
	@Description nvarchar(255),
	@UserId int
AS
Begin
	insert into ToDo (Title, [Description], UserId) output inserted.Id values (@Title, @Description, @UserId);
End
