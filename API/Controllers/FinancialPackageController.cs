using System;
using Domain.Model;
using Persistence.Repository;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class FinancialPackageController : ControllerBase
    {
        #region constructor and fields

        private readonly IFinancialPackage _financialPackage;

        public FinancialPackageController(IFinancialPackage financialPackage)
        {
            _financialPackage = financialPackage;
        }

        #endregion


        [HttpGet]
        public ActionResult<IEnumerable<FinancialPackage>> GetFinancials()
        {
            var financialPackages = _financialPackage.GetAll();
            var json = JsonSerializer.Serialize(financialPackages);
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<FinancialPackage> GetFinancialById(int id) =>
            await _financialPackage.GetByIdAsync(id);


        [HttpPost]
        public async Task<ActionResult> CreateFinancialPackage(FinancialDTO createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("All filds are requird!");

            var financial = new FinancialPackage();
            financial.Term = createDTO.Term;
            financial.ProfitPercent = createDTO.ProfitPercent;

            try
            {
                await _financialPackage.CreateAsync(financial);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFinancialPackage(int id)
        {
            if (id > 0)
                return BadRequest("id is not valid");

            try
            {
                var res = await _financialPackage.Delete(id);
                if (res)
                    return Ok();
                else
                    return BadRequest("id is not valid");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateFinancial(FinancialDTO financialDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("All fields are required!");

            var finance = new FinancialPackage();
            finance.Id = id;
            finance.ProfitPercent = financialDTO.ProfitPercent;
            finance.Term = financialDTO.Term;

            try
            {
                _financialPackage.UpdateAsync(finance);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
