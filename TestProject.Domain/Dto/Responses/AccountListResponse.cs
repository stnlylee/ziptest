using System.Collections.Generic;

namespace TestProject.Domain.Dto.Responses
{
    public class AccountListResponse
    {
        public IEnumerable<AccountResponse> AccountList { get; set; }
    }
}
