using GestToDo.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestToDo.Web.Controllers
{
    [IsLoggedAction]
    public class ControllerBase : Controller
    {
        protected internal ISessionManager SessionManager { get; private set; }

        public ControllerBase(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }
    }
}
