CREATE PROCEDURE [ToDoApp].[SP_GetUserToDo]
	@UserId int
AS
Begin
	SELECT Id, Title, [Description], Done, ValidationDate, UserId
	From ToDo
	Where UserId = @UserId;
End
