﻿#region using
using System;
using MediatR;
using Domain.Model;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Application.FinancialPackages;
using Microsoft.AspNetCore.Authorization;
using Application;
#endregion

namespace API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class FinancialPackageController : ControllerBase
    {
        #region ctor
        private readonly IMediator _mediator;

        public FinancialPackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        [HttpGet]
        public ActionResult<IEnumerable<FinancialPackage>> GetFinancials()
        {
            var financialPackages = _mediator.Send(new GetAllFinancialPackagesAsync.Query());
            var json = JsonSerializer.Serialize(financialPackages);
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<FinancialPackage> GetFinancialById(int id) =>
            await _mediator.Send(new FindFinancialPackageByIdAsync.Query(id));



        [HttpPost]
        public async Task<ActionResult> CreateFinancialPackage(FinancialDTO financialDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("All filds are requird!");

            FinancialPackage financialPackage = new();
            financialPackage.ProfitPercent = financialDTO.ProfitPercent;
            financialPackage.Term = financialDTO.Term;

            try
            {
                await _mediator.Send(new CreateFinancialPackageAsync.Command(financialPackage));
                await _mediator.Send(new Save.Command());
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
            if (id < 0)
                return BadRequest("id is not valid");

            try
            {
                var res = await _mediator.Send(new RemoveFinancialPackages.Command(id));

                await _mediator.Send(new Save.Command());

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFinancial(FinancialDTO financialDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("All fields are required!");

            var financialPackageFromDb = await _mediator.Send(new FindFinancialPackageByIdAsync.Query(id));

            financialPackageFromDb.ProfitPercent = financialDTO.ProfitPercent;
            financialPackageFromDb.Term = financialDTO.Term;

            try
            {
                await _mediator.Send(new UpdateFinancialPackagesAsync.Command(financialPackageFromDb));
                
                await _mediator.Send(new Save.Command());

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
