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
            CreateMap<RegisterDto, AppUser>()
                .ForMember(cd => cd.RegisterDate, opt =>
                    opt.MapFrom(_ => DateTime.Now));

            CreateMap<FinancialDTO, FinancialPackage>();

            CreateMap<FinancialPackage, FinancialPackage>();

        }
    }
}
