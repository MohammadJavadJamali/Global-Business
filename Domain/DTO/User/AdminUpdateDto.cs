using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AdminUpdateDto
    {

        public string UserName { get; set; }


        [Required]
        public ICollection<string> Roles { get; set; }

    }
}
