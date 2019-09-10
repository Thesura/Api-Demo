using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_API.Models
{
    public class MemberForCreationDto
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
