using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BusinessService.User;
using TestProject.Domain.Dto.Requests;
using TestProject.Domain.Dto.Responses;
using TestProject.Domain.Models;

namespace TestProject.WebAPI.Controllers.v1
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public UserController(IUserService userService,
            ILogger<UserController> logger,
            IMapper mapper,
            IMemoryCache cache)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet("/v1/[controller]s", Name = "GetAllUsers")]
        public async Task<ActionResult<UserListResponse>> GetAllUsers()
        {
            if (!_cache.TryGetValue("users", out IEnumerable<User> users))
            {
                _logger.LogInformation("user cache miss");
                users = await _userService.GetAllUsers();
                _cache.Set("users", users, TimeSpan.FromSeconds(30));
            }

            return Ok(new UserListResponse
            {
                UserList = _mapper.Map<IEnumerable<User>,
                IEnumerable<UserResponse>>(users)
            });
        }

        [HttpGet("/v1/[controller]", Name = "GetUserByEmail")]
        public async Task<ActionResult<UserWithAccountsResponse>> GetUserByEmail([FromQuery]string email)
        {
            var user = await _userService.GetUserByEmail(email);
            if (user != null)
                return Ok(_mapper.Map<User, UserWithAccountsResponse>(user));
            return NotFound();
        }

        [HttpPost("/v1/[controller]", Name = "CreateUser")]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateUser(_mapper.Map<CreateUserRequest, User>(request));
            if (user == null)
                return BadRequest(new ErrorRespose { ErrorMessage = "user exists" });
            return Ok(_mapper.Map<User, UserResponse>(user));

        }
    }
}
