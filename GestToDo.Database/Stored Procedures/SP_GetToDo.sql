CREATE PROCEDURE [ToDoApp].[SP_GetToDo]
	@Id int,
	@UserId int
AS
Begin
	SELECT Id, Title, [Description], Done, ValidationDate, UserId
	From ToDo
	Where UserId = @UserId And Id = @Id;
End
