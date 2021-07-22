using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class AppUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public bool CommissionPaid { get; set; }

        public List<Profit> Profits { get; set; }

        public DateTime RegisterDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal AccountBalance { get; set; }

        public string IntroductionCode { get; set; }




        public Node Node { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public List<UserFinancialPackage> UserFinancialPackages { get; set; }

    }
}