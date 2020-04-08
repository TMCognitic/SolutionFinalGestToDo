using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestToDo.Forms;
using GestToDo.Interfaces;
using GestToDo.Models.Data;
using GestToDo.Web.Infrastructure;
using GestToDo.Web.Models.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestToDo.Web.Controllers
{
    [AuthRequired]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository<ToDo> _toDoRepository;

        public ToDoController(IToDoRepository<ToDo> toDoRepository, ISessionManager sessionManager) : base(sessionManager)
        {
            _toDoRepository = toDoRepository;
        }

        // GET: ToDo
        public ActionResult Index()
        {
            return View(_toDoRepository.Get(SessionManager.User.Id));
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int id)
        {
            return View(_toDoRepository.Get(SessionManager.User.Id, id));
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateToDo form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toDoRepository.Insert(new ToDo(form.Title, form.Description, SessionManager.User.Id));
                    return RedirectToAction("Index");
                }

                return View(form);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int id)
        {
            ToDo td = _toDoRepository.Get(SessionManager.User.Id, id);

            return View(new EditToDo() { Id = td.Id, Title = td.Title, Description = td.Description, Done = td.Done });
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditToDo form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toDoRepository.Update(id, new ToDo(id, form.Title, form.Description, form.Done, null, SessionManager.User.Id));
                    return RedirectToAction("Index");
                }

                return View(form);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: ToDo/Delete/5
        public ActionResult Delete(int id)
        {
            if (SessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _toDoRepository.Delete(SessionManager.User.Id, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}