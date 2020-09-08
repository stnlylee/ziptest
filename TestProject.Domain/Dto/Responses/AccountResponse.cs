namespace TestProject.Domain.Dto.Responses
{
    public class AccountResponse : AccountDtoBase
    {
        public UserResponse User { get; set; }
    }
}
