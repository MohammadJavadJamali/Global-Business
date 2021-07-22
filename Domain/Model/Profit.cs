using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Profit : BaseEntity
    {

        public DateTime ProfitDepositDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ProfitAmount { get; set; }


        public string User_Id { get; set; }
        public AppUser User { get; set; }

    }
}
