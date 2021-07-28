using AutoMapper;
using Domain.Model;

namespace Application
{
    public class MapingProfiles : Profile
    {
        public MapingProfiles()
        {
            CreateMap<AppUser, AppUser>();

            CreateMap<FinancialPackage, FinancialPackage>();

            CreateMap<UserFinancialPackage, UserFinancialPackage>();

            CreateMap<Node, Node>();
        }
    }
}
