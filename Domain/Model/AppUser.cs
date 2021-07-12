using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AccountBalance { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public IList<UserFinancialPackage> UserFinancialPackages { get; set; }

        public List<Profit> Profits { get; set; }


    }
}
