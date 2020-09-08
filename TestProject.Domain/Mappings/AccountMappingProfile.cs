using AutoMapper;
using TestProject.Domain.Dto.Requests;
using TestProject.Domain.Dto.Responses;
using TestProject.Domain.Models;

namespace TestProject.Domain.Mappings
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            // dto -> domain
            CreateMap<CreateAccountRequest, Account>();
            CreateMap<AccountResponse, Account>();
            CreateMap<AccountWithNoUserResponse, Account>();
            // domain -> dto
            CreateMap<Account, CreateAccountRequest>();
            CreateMap<Account, AccountResponse>();
            CreateMap<Account, AccountWithNoUserResponse>();
        }
    }
}
