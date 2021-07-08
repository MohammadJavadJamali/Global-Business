using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class UserFinancialPackage
    {
        [Key]
        public int Id { get; set; }

        public DateTime ChoicePackageDate { get; set; }

        public DateTime EndFinancialPackageDate { get; set; }

        public decimal AmountInPackage { get; set; }

        public bool IsDeleted { get; set; }

        public virtual AppUser User { get; set; }
        public string UserId { get; set; }

        public virtual FinancialPackage FinancialPackage { get; set; }
        public int FinancialPackageId { get; set; }

    }
}
