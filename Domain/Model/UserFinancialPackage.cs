using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class UserFinancialPackage
    {
        public int Id { get; set; }

        public DateTime ChoicePackageDate { get; set; }

        public DateTime EndFinancialPackageDate { get; set; }

        public decimal AmountInPackage { get; set; }

        public bool IsDeleted { get; set; }

        public virtual AppUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }


        public virtual FinancialPackage FinancialPackage { get; set; }
        [ForeignKey("FinancialPackage")]
        public int FinancialPackageId { get; set; }

    }
}
