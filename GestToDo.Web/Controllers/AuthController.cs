using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestToDo.Forms;
using GestToDo.Interfaces;
using GestToDo.Models.Global;
using GestToDo.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository<RegisterForm, LoginForm, User> _authRepository;
        private readonly ISessionManager _sessionManager;

        public AuthController(IAuthRepository<RegisterForm, LoginForm, User> authRepository, ISessionManager sessionManager)
        {
            _authRepository = authRepository;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    User user = _authRepository.Login(loginForm);

                    if (!(user is null))
                    {
                        _sessionManager.User = user;
                        return RedirectToAction("Index", "ToDo");
                    }

                    ViewBag.Message = "Incorrect login or password!";
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }

            return View(loginForm);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterForm registerForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authRepository.Register(registerForm);
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }

            return View(registerForm);
        }

        public IActionResult Logout()
        {            
            _sessionManager.Abandon();
            return RedirectToAction("Login");
        }
    }
}