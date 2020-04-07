CREATE PROCEDURE [ToDoApp].[SP_LoginUser]
	@Email nvarchar(384),
	@Passwd nvarchar(20)
AS
Begin
	Select Id, LastName, FirstName, Email 
	From [User]
	Where Email = @Email And Passwd = @Passwd
End