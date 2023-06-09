using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.CloseAccount;
using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.DepositeFund;
using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.OpenAccount;
using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.WithdrawnFund;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Banking.Account.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BankAccountOperationAccountController: ControllerBase
    {
        private IMediator _mediator;

        public BankAccountOperationAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("OpenAccount", Name = "OpenAccount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> OpenAccount([FromBody] OpenAccountsCommand command)
        {
            var id = Guid.NewGuid().ToString();
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("CloseAccount/{id}", Name = "CloseAccount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CloseAccount(string id)
        {
            var command = new CloseAccountCommand
            {
                Id = id
            };
            return await _mediator.Send(command);
        }

        [HttpPut("DepositFund/{id}", Name = "DepositFund")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DepositFund(string id, [FromBody] DepositeFundCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpPut("WithdrawnFunds/{id}", Name = "WithdrawnFunds")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> WithdrawnFunds(string id, [FromBody] WithdrawnFundCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

    }


}
