using System;
using AutoMapper;
using Domain.DTO;
using Domain.Model;
using Domain.DTO.FinancialDTO;

namespace Application.Infrastructure.Mapper
{
    public class DomainProfile : Profile    
    {
        public DomainProfile()
        {

            CreateMap<FinancialDTO, FinancialPackage>();

            CreateMap<FinancialPackage, FinancialPackage>();

        }
    }
}
