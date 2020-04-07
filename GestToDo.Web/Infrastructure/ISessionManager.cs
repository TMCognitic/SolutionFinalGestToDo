using GestToDo.Models.Global;

namespace GestToDo.Web.Infrastructure
{
    public interface ISessionManager
    {
        User User { get; set; }

        void Abandon();
    }
}