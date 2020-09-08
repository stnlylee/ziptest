using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.BusinessService.User
{
    public interface IUserService
    {
        Task<IEnumerable<Domain.Models.User>> GetAllUsers();
        Task<Domain.Models.User> GetUserByEmail(string email);
        Task<Domain.Models.User> GetUserById(int id);
        Task<Domain.Models.User> CreateUser(Domain.Models.User user);
        Task<bool> CheckCredit(Domain.Models.User user);
    }
}
