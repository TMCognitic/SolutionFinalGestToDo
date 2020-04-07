CREATE PROCEDURE [ToDoApp].[SP_DeleteToDo]
	@Id int,
	@UserId int
AS
Begin
	Delete From ToDo Where Id = @Id And UserId = @UserId;
End
