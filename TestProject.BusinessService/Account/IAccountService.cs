using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.BusinessService.Account
{
    public interface IAccountService
    {
        Task<IEnumerable<Domain.Models.Account>> GetAllAccounts();
        Task<Domain.Models.Account> CreateAccount(int userId, decimal initCredit);
    }
}
