using System.Collections.Generic;

namespace TestProject.Domain.Dto.Responses
{
    public class UserWithAccountsResponse : UserDtoBase
    {
        public IEnumerable<AccountWithNoUserResponse> Accounts { get; set; }
    }
}
