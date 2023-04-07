﻿using CompanyAdmin.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO emailDTO); 
    }
}
