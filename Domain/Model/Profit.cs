using System;

namespace Domain.Model
{
    public class Profit : BaseEntity
    {

        public DateTime ProfitDepositDate { get; set; }

        public decimal ProfitAmount { get; set; }


        public string User_Id { get; set; }
        public AppUser User { get; set; } = new();

    }
}
