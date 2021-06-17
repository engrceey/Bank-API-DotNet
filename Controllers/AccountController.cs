using System.Text.RegularExpressions;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniBankApi.Dto;
using MiniBankApi.models;
using MiniBankApi.Services;

namespace MiniBankApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _mapper = mapper;
            _accountService = accountService;

        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNewAccout([FromBody] RegisterNewAccountModel model) 
        {
            if (!ModelState.IsValid) return BadRequest(model); 

            var account = _mapper.Map<Account>(model);
            return Ok(_accountService.Create(account, model.Pin, model.ConfirmPin)); 

        }

        [HttpGet]
        [Route("get_all_accounts")]
        public IActionResult GetAllAccounts() 
        {
            var accounts = _accountService.GetAllAccounts();

            var accountsToReturn = _mapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(accountsToReturn);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));
            
        }

        [HttpGet]
        [Route("get_by_account_number")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^\d{9}[0-9]$")) return BadRequest("Your Account Number can only be 10 digits");

            var account = _accountService.GetByAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet]
        [Route("get_account_by_Id")]
        public IActionResult GetByAccountById(int Id)
        {
           
            var account = _accountService.GetById(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }


        [HttpPut]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<Account>(model);
        
            _accountService.Update(account, model.Pin);

            return Ok();

        }
        
    }
}