CREATE PROCEDURE [ToDoApp].[SP_UpdateToDo]
	@Id int,
	@Title nvarchar(128),
	@Description nvarchar(255),
	@Done bit,
	@UserId int
AS
Begin
	Update ToDo Set Title = @Title, [Description] = @Description, Done = @Done where Id = @Id And UserId = @UserId;
End
