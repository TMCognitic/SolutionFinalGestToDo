using GestToDo.Api.Models.Mappers;
using GestToDo.Forms;
using GestToDo.Interfaces;
using GestToDo.Models.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using ToolBox.Connections;

namespace GestToDo.Api.Models.Repositories
{
    public class AuthRepository : IAuthRepository<RegisterForm, LoginForm, User>
    {
        private IConnection _dbConnection;

        public AuthRepository()
        {
            _dbConnection = new Connection(ConfigurationManager.ConnectionStrings["GestToDoDb"].ConnectionString, DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["GestToDoDb"].ProviderName));
        }

        public User Login(LoginForm loginForm)
        {
            Command command = new Command("ToDoApp.SP_LoginUser", true);
            command.AddParameter("Email", loginForm.Email);
            command.AddParameter("Passwd", loginForm.Passwd);

            return _dbConnection.ExecuteReader(command, dr => dr.ToUser()).SingleOrDefault();
        }

        public void Register(RegisterForm registerForm)
        {
            Command command = new Command("ToDoApp.SP_RegisterUser", true);
            command.AddParameter("LastName", registerForm.LastName);
            command.AddParameter("FirstName", registerForm.FirstName);
            command.AddParameter("Email", registerForm.Email);
            command.AddParameter("Passwd", registerForm.Passwd);

            _dbConnection.ExecuteNonQuery(command);
        }
    }
}