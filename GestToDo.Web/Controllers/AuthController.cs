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
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository<RegisterForm, LoginForm, User> _authRepository;

        public AuthController(IAuthRepository<RegisterForm, LoginForm, User> authRepository, ISessionManager sessionManager) : base(sessionManager)
        {
            _authRepository = authRepository;
        }

        [AnonymousRequired]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [AnonymousRequired]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AnonymousRequired]
        public IActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    User user = _authRepository.Login(loginForm);

                    if (!(user is null))
                    {
                        SessionManager.User = user;
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

        [AnonymousRequired]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AnonymousRequired]
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

        [AuthRequired]
        public IActionResult Logout()
        {            
            SessionManager.Abandon();
            return RedirectToAction("Login");
        }
    }
}