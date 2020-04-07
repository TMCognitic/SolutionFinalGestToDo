using GestToDo.Api.Models.Mappers;
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
    public class ToDoRepository : IToDoRepository<ToDo>
    {
        private IConnection _dbConnection;

        public ToDoRepository()
        {
            _dbConnection = new Connection(ConfigurationManager.ConnectionStrings["GestToDoDb"].ConnectionString, DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["GestToDoDb"].ProviderName));
        }

        public IEnumerable<ToDo> Get(int userId)
        {
            Command command = new Command("ToDoApp.SP_GetUserToDo", true);
            command.AddParameter("UserId", userId);

            return _dbConnection.ExecuteReader(command, dr => dr.ToToDo());
        }

        public ToDo Get(int userId, int id)
        {
            Command command = new Command("ToDoApp.SP_GetToDo", true);
            command.AddParameter("UserId", userId);
            command.AddParameter("Id", id);

            return _dbConnection.ExecuteReader(command, dr => dr.ToToDo()).SingleOrDefault();
        }

        public ToDo Insert(ToDo entity)
        {
            Command command = new Command("ToDoApp.SP_CreateToDo", true);
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Description", entity.Description);
            command.AddParameter("UserId", entity.UserId);

            entity.Id = (int)_dbConnection.ExecuteScalar(command);
            return entity;
        }

        public bool Update(int id, ToDo entity)
        {
            Command command = new Command("ToDoApp.SP_UpdateToDo", true);
            command.AddParameter("Id", id);
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Description", entity.Description);
            command.AddParameter("Done", entity.Done);
            command.AddParameter("UserId", entity.UserId);

            return _dbConnection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int userId, int id)
        {
            Command command = new Command("ToDoApp.SP_DeleteToDo", true);
            command.AddParameter("Id", id);
            command.AddParameter("UserId", userId);

            return _dbConnection.ExecuteNonQuery(command) == 1;
        }
    }
}