using System;
using AutoMapper;
using Domain.Model;
using System.Text.Json;
using Persistence.Repository;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class FinancialPackageController : ControllerBase
    {
        #region constructor and fields

        private readonly IMapper _mapper;
        private readonly IFinancialPackage _financialPackage;

        public FinancialPackageController(
              IMapper mapper
            , IFinancialPackage financialPackage)
        {
            _mapper = mapper;
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

            var financial = _mapper.Map<FinancialPackage>(createDTO);

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
                var res = await _financialPackage.DeleteAsync(id);
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
        public async Task<ActionResult> UpdateFinancial(FinancialPackage financialPackage, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("All fields are required!");

            var financialPackageFromDb = await _financialPackage.GetByIdAsync(id);
            
            financialPackage.Id = id;
            _mapper.Map(financialPackage, financialPackageFromDb);

            try
            {
                _financialPackage.UpdateAsync(financialPackageFromDb);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
