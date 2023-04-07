using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class ChangePasswordInputModel
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The NewPassword and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
