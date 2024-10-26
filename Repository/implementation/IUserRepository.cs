using webApi_build_Real.Models;

namespace webApi_build_Real.Repository.implementation
{
    public interface IUserRepository
    {
        Task<Usercs> Authenticate(string userName, String password);
        void Register(string userName, string password);
        Task<bool> UserAlreadyExists(string userName);
    }
}
