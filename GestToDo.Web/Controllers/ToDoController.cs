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
    public class ToDoController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IToDoRepository<ToDo> _toDoRepository;

        public ToDoController(IToDoRepository<ToDo> toDoRepository, ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _toDoRepository = toDoRepository;
        }

        // GET: ToDo
        public ActionResult Index()
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            return View(_toDoRepository.Get(_sessionManager.User.Id));
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int id)
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            return View(_toDoRepository.Get(_sessionManager.User.Id, id));
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateToDo form)
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            try
            {
                if (ModelState.IsValid)
                {
                    _toDoRepository.Insert(new ToDo(form.Title, form.Description, _sessionManager.User.Id));
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
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            ToDo td = _toDoRepository.Get(_sessionManager.User.Id, id);

            return View(new EditToDo() { Id = td.Id, Title = td.Title, Description = td.Description, Done = td.Done });
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditToDo form)
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            try
            {
                if (ModelState.IsValid)
                {
                    _toDoRepository.Update(id, new ToDo(id, form.Title, form.Description, form.Done, null, _sessionManager.User.Id));
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
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            if (_sessionManager.User is null)
                return RedirectToAction("Login", "Auth");

            try
            {
                _toDoRepository.Delete(_sessionManager.User.Id, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}