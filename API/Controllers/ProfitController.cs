using MediatR;
using Domain.DTO;
using System.Linq;
using Domain.Model;
using System.Text.Json;
using Application.Profits;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ProfitController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Method

        [HttpPost]
        public async Task CreateProfit(ProfitDTO profitDTO)
        {
            Profit profit = new();
            profit.ProfitAmount = profitDTO.ProfitAmount;
            profit.User = profitDTO.AppUser;

            await _mediator.Send(new CreateProfitAsync.Command(profit));
        }

        [HttpGet]
        public async Task<IActionResult> GetProfitByDateFilter(ProfitDateFilter profitDateFilter)
        {
            //IEnumerable<Profit> profits = await _profit
            //    .GetAll(p => p.ProfitDepositDate >= profitDateFilter.StartDate &&
            //                p.ProfitDepositDate <= profitDateFilter.EndDate);

            List<Profit> profits = await _mediator.Send(new GetAllProfitsAsync.Query());

            profits = profits.Where(p => p.ProfitDepositDate >= profitDateFilter.StartDate &&
                            p.ProfitDepositDate <= profitDateFilter.EndDate).ToList();

            if (profits is null)
                return BadRequest();

            var json = JsonSerializer.Serialize(profits);

            return Ok(json);
        }


        #endregion

    }
}
