using System.Collections.Generic;

namespace Domain.Model
{
    public class FinancialPackage : BaseEntity
    {
        public FinancialPackage()
        {
            UserFinancialPackages = new List<UserFinancialPackage>();
        }

        public float ProfitPercent { get; set; }

        public int Term { get; set; }


        //about relation
        public virtual List<UserFinancialPackage> UserFinancialPackages { get; set; }

    }
}
