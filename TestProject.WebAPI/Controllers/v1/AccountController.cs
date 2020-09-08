using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.BusinessService.Account;
using TestProject.Domain.Dto.Requests;
using TestProject.Domain.Dto.Responses;
using TestProject.Domain.Models;

namespace TestProject.WebAPI.Controllers.v1
{
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _accountService = accountService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("/v1/[controller]s", Name = "GetAllAccounts")]
        public async Task<ActionResult<AccountListResponse>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();

            return Ok(new AccountListResponse
            {
                AccountList = _mapper.Map<IEnumerable<Account>,
                    IEnumerable<AccountResponse>>(accounts)
            });
        }

        [HttpPost("/v1/[controller]", Name = "CreateAccount")]
        public async Task<ActionResult<AccountResponse>> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var account = await _accountService.CreateAccount(request.UserId, request.Credit);
            if (account == null)
                return BadRequest(new ErrorRespose { ErrorMessage = "create account failed" });
            return Ok(_mapper.Map<Account, AccountResponse>(account));
        }
    }
}
