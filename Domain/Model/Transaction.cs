using System;

namespace Domain.Model
{
    public class Transaction : BaseEntity
    {

        public decimal InitialBalance { get; set; }
               
        public decimal Amount { get; set; }
               
        public decimal FinalBalance { get; set; }

        public DateTime TransactionDate { get; set; }

        public string EmailTargetAccount { get; set; }

        // about relation
        public string User_Id { get; set; }

        public virtual AppUser User { get; set; }


    }
}
