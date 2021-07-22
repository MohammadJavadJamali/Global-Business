using Domain.DTO;
using Domain.Model;
using System.Text.Json;
using Application.Repository;
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

        #region Fields

        private readonly IProfit _profit;

        #endregion

        #region Ctor
        public ProfitController(IProfit profit)
        {
            _profit = profit;
        }

        #endregion

        #region Method

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


        #endregion

    }
}
