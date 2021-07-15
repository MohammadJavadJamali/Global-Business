using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class AppUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public List<Profit> Profits { get; set; }

        public DateTime RegisterDate { get; set; }

        public decimal AccountBalance { get; set; }

        public string IntroductionCode { get; set; }


        public Node Node { get; set; } = new();

        public virtual List<Transaction> Transactions { get; set; }

        public List<UserFinancialPackage> UserFinancialPackages { get; set; }

    }
}