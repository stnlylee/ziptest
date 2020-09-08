using System.Collections.Generic;

namespace TestProject.Domain.Dto.Responses
{
    public class UserListResponse
    {
        public IEnumerable<UserResponse> UserList { get; set; }
    }
}
