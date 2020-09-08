using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Domain.Repositories;

namespace TestProject.BusinessService.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckCredit(Domain.Models.User user)
        {
            if (user == null)
                return false;
            if (user.MonthlySalary - user.MonthlyExpense >= 1000)
                return true;

            return false;
        }

        public async Task<Domain.Models.User> CreateUser(Domain.Models.User user)
        {
            var existing = await GetUserByEmail(user.Email);
            if (existing != null)
                return null;
            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<Domain.Models.User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Domain.Models.User> GetUserByEmail(string email)
        {
            var searchResult =  await _userRepository.SearchAsync(u => u.Email == email);
            return searchResult.FirstOrDefault();
        }

        public async Task<Domain.Models.User> GetUserById(int id)
        {
            if (id <= 0)
                return null;
            return await _userRepository.GetAsync(id);
        }
    }
}
