using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.Sprint.Input
{
    public class AddSprintInputModel
    {
        public string SprintName { get; set; }
        public string Goal { get; set; }
        public string Duration { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
