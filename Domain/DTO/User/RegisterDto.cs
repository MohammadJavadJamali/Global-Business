using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class RegisterDto
    {

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(13)]
        [MinLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string PhonNumber { get; set; }

        [Required]
        [MinLength(9)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public decimal AccountBalance { get; set; }

        [Required]
        public ICollection<string> Roles { get; set; }

    }
}
