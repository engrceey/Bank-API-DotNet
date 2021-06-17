using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniBankApi.Dto;
using MiniBankApi.models;
using MiniBankApi.Services.Interface;

namespace MiniBankApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionService;

        private readonly IMapper _mapper;

        public TransactionController(ITransaction transactionService,
                                    IMapper mapper)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("create_new_transaction")]
        public IActionResult CreateNewTransaction([FromBody] TransactionRequestDto transactionRequest)
        {
            if (!ModelState.IsValid) return BadRequest(transactionRequest);

            var transaction = _mapper.Map<Transaction>(transactionRequest);
            return Ok(_transactionService.CreateNewTransaction(transaction));
        }

        [HttpPost]
        [Route("make_deposit")]
        public IActionResult MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^\d{9}[0-9]$")) return BadRequest("Your Account Number can only be 10 digits");
            return Ok(_transactionService.MakeDeposit(AccountNumber,Amount,TransactionPin));
        }

        [HttpPost]
        [Route("make_withdrawal")]
        public IActionResult MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^\d{9}[0-9]$")) return BadRequest("Your Account Number can only be 10 digits");
            return Ok(_transactionService.MakeWithdrawal(AccountNumber,Amount,TransactionPin));
        }

        [HttpPost]
        [Route("make_funds_transfer")]
        public IActionResult MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            if (!(Regex.IsMatch(FromAccount, @"^\d{9}[0-9])$") || !(Regex.IsMatch(ToAccount, @"^\d{9}[0-9])$"))))
                return BadRequest("Your Account Number can only be 10 digits");
            return Ok(_transactionService.MakeFundsTransfer(FromAccount,ToAccount,Amount,TransactionPin));
        }

    }
}