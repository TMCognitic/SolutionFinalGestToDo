CREATE ROLE [AppUser]
Go

GRANT EXECUTE On SCHEMA::[ToDoApp] TO [AppUser];
Go

Alter Role [AppUser]
Add Member [GestToDo];
Go

