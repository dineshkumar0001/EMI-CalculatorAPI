using EmiCalculator.Models;
using EmiCalculator.Request_Models;

namespace EmiCalculator.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);
    }


}
