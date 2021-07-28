

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Node
    {

        public int Id { get; set; }

        public string ParentId { get; set; }

        public string LeftUserId { get; set; }

        public string RightUserId { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalMoneyInvested { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalMoneyInvestedBySubsets { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal MinimumSubBrachInvested { get; set; }

        public string IntroductionCode { get; set; }

        public bool IsCalculate { get; set; }

        public string UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
