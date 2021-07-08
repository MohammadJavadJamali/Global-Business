using System;
using Domain.Model;

namespace Domain.DTO
{
    public class ProfitDTO
    {
        public decimal ProfitAmount { get; set; }
        public AppUser AppUser { get; set; }

    }
}
