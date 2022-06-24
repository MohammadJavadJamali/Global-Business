using Domain.DTO;
using Domain.Model;
using System.Text.Json;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ProfitController : ControllerBase
    {
        private readonly IProfit _profit;

        public ProfitController(IProfit profit)
        {
            _profit = profit;
        }

        [HttpPost]
        public async Task CreateProfit(ProfitDTO profitDTO)
        {
            Profit profit = new();
            profit.ProfitAmount = profitDTO.ProfitAmount;
            profit.User = profitDTO.AppUser;

            await _profit.CreateAsync(profit);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfitByDateFilter(ProfitDateFilter profitDateFilter)
        {
            IEnumerable<Profit> profits = await _profit
                .GetAll(p => p.ProfitDepositDate >= profitDateFilter.StartDate &&
                            p.ProfitDepositDate <= profitDateFilter.EndDate);


            if (profits is null)
                return BadRequest();

            var json = JsonSerializer.Serialize(profits);

            return Ok(json);
        }
    }
}
