using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.Channel.Input
{
    public class AddChannelInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
