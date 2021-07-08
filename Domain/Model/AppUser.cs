using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            UserFinancialPackages = new List<UserFinancialPackage>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AccountBalance { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool HaveFinancialPackage { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<UserFinancialPackage> UserFinancialPackages { get; set; }

        public List<Profit> Profits { get; set; }


    }
}
