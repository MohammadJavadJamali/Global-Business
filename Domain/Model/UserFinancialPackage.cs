using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class UserFinancialPackage
    {
        public DateTime ChoicePackageDate { get; set; }

        public DateTime EndFinancialPackageDate { get; set; }

        public int DayCount { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public decimal AmountInPackage { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ProfitAmountPerDay { get; set; }

        public bool IsDeleted { get; set; }


        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public int FinancialPackageId { get; set; }
        public virtual FinancialPackage FinancialPackage { get; set; } 


    }
}
