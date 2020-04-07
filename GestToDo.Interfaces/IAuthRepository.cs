using System;

namespace GestToDo.Interfaces
{
    public interface IAuthRepository<TRegisterForm, TLoginForm, TResult>
    {
        TResult Login(TLoginForm loginForm);
        void Register(TRegisterForm registerForm);        
    }
}
