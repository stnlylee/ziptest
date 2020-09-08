using AutoMapper;
using TestProject.Domain.Dto;
using TestProject.Domain.Dto.Requests;
using TestProject.Domain.Dto.Responses;
using TestProject.Domain.Models;

namespace TestProject.Domain.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // dto -> domain
            CreateMap<CreateUserRequest, User>();
            CreateMap<UserResponse, User>();
            CreateMap<UserWithAccountsResponse, User>();
            // domain -> dto
            CreateMap<User, CreateUserRequest>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserWithAccountsResponse>();
        }
    }
}
