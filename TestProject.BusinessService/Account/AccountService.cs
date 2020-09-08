using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BusinessService.User;
using TestProject.Domain.Repositories;

namespace TestProject.BusinessService.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;

        public AccountService(
            IAccountRepository accountRepository, 
            IUserService userService)
        {
            _accountRepository = accountRepository;
            _userService = userService;
        }

        public async Task<Domain.Models.Account> CreateAccount(int userId,
            decimal initCredit)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
                return null;

            if (await _userService.CheckCredit(user))
            {
                return await _accountRepository.AddAsync(new Domain.Models.Account
                {
                    User = user,
                    Credit = initCredit
                });
            }

            return null;
        }

        public async Task<IEnumerable<Domain.Models.Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAsync();
        }
    }
}
