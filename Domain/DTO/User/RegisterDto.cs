using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhonNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal AccountBalance { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
