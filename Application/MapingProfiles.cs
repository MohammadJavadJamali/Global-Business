using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MapingProfiles : Profile
    {
        public MapingProfiles()
        {
            CreateMap<AppUser, AppUser>();

            CreateMap<FinancialPackage, FinancialPackage>();

            CreateMap<Node, Node>();
        }
    }
}
