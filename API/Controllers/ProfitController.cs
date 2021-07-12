using Domain.DTO;
using System.Linq;
using Domain.Model;
using System.Text.Json;
using Persistence.Repository;
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

        #region constructor and fields

        private readonly IProfit _profit;

        public ProfitController(IProfit profit)
        {
            _profit = profit;
        }

        #endregion

        #region Method

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
