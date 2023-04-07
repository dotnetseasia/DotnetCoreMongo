using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Common
{
    public class ServiceResponse
    {
        public bool? isError { get; set; } = false;
        public List<string> listErrors { get; set; } = new List<string>();
        public object result { get; set; }
    }
}
