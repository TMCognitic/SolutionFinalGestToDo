using GestToDo.Models.Global;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestToDo.Web.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public User User
        {
            get
            {
                string json = _session.GetString(nameof(User));
                return (json is null) ? null : JsonConvert.DeserializeObject<User>(json);
            }
            set
            {
                _session.SetString(nameof(User), JsonConvert.SerializeObject(value));
            }
        }

        public void Abandon()
        {
            _session.Clear();
        }
    }
}
