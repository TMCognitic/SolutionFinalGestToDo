using GestToDo.Api.Models;
using GestToDo.Api.Models.Repositories;
using GestToDo.Interfaces;
using GestToDo.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestToDo.Api.Controllers
{
    public class ToDoController : ApiController
    {
        private IToDoRepository<ToDo> _todoRepository;

        public ToDoController()
        {
            _todoRepository = new ToDoRepository();
        }

        // GET: api/ToDo
        [Route("api/Todo/GetByUserId/{userId:int}")]
        public IEnumerable<ToDo> Get(int userID)
        {
            return _todoRepository.Get(userID);
        }

        // GET: api/ToDo/5
        [Route("api/Todo/{userId:int}/{id:int}")]
        public ToDo Get(int userID, int id)
        {
            return _todoRepository.Get(userID, id);
        }

        // POST: api/ToDo
        public ToDo Post([FromBody]CreateToDo toDo)
        {
            ToDo td = new ToDo() { Title = toDo.Title, Description = toDo.Description, UserId = toDo.UserId };
            return _todoRepository.Insert(td);
        }

        // PUT: api/ToDo/5
        public HttpResponseMessage Put(int id, [FromBody]ToDo toDo)
        {
            if (_todoRepository.Update(id, toDo))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // DELETE: api/ToDo/5
        [Route("api/Todo/{userId:int}/{id:int}")]
        public HttpResponseMessage Delete(int userId, int id)
        {
            if (_todoRepository.Delete(userId, id))
                return new HttpResponseMessage(HttpStatusCode.OK);
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
