using System.Collections.Generic;

namespace Domain.Model
{
    public class FinancialPackage : BaseEntity
    {

        public float ProfitPercent { get; set; }

        public int Term { get; set; }


        public List<UserFinancialPackage> UserFinancialPackages { get; set; }

    }
}
