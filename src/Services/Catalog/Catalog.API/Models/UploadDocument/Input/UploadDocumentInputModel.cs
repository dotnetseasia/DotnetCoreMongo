using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.UploadDocument.Input
{
    public class UploadDocumentInputModel
    {
        [Required]
       public IFormFile File { get; set; }
        [Required]
        public string FileFor { get; set; }
    }
}
