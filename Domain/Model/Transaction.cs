using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Transaction : BaseEntity
    {

        [Column(TypeName = "decimal(18,4)")]
        public decimal InitialBalance { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal FinalBalance { get; set; }

        public DateTime TransactionDate { get; set; }

        public string EmailTargetAccount { get; set; }

        // about relation
        public string User_Id { get; set; }

        public virtual AppUser User { get; set; } 


    }
}
