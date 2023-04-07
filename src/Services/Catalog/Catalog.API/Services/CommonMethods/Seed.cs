using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.CommonMethods
{
    public class Seed
    {
        private IConfiguration _configuration { get; }
        private readonly IApplicationConfigurationService _applicationConfigurationService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IApplicationConfigurationRepository _applicationConfigurationRepository;  

        public Seed(
        IConfiguration configuration,
        IEmailTemplateService emailTemplateService, 
        IApplicationConfigurationService applicationConfigurationService,
        IApplicationConfigurationRepository applicationConfigurationRepository)
        {
            _configuration = configuration;
            _emailTemplateService = emailTemplateService; 
            _applicationConfigurationService = applicationConfigurationService;
            _applicationConfigurationRepository = applicationConfigurationRepository;
        }

        #region Application Configurations 
        public void HydrateApplicationConfigurations()
        {
            var colApplicaitonConfiguration = _applicationConfigurationRepository.GetApplicationConfigurations();
            var appConfigs = Map(colApplicaitonConfiguration);

            IDictionary<string, object> dictionary = appConfigs.GetType()
                                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(appConfigs, null));

            ConcurrentDictionary<string, object> concurrentDictionary = new ConcurrentDictionary<string, object>(dictionary);
            _applicationConfigurationService.Set(concurrentDictionary);
        }

        private ApplicationConfigurations Map(ColApplicaitonConfiguration colApplicaitonConfiguration)
        {
            return new ApplicationConfigurations()
            {
                CredentialExpiryDays = colApplicaitonConfiguration.PasswordExpiryDays,
                PrePasswordExpiryNotificationDays = colApplicaitonConfiguration.PrePasswordExpiryNotificationDays,
                MaxLoginAttempts = colApplicaitonConfiguration.MaxLoginAttempts,
                UserLockedDuration = colApplicaitonConfiguration.UserLockedDuration,
                RestrictedUsedPasswordCount = colApplicaitonConfiguration.RestrictedUsedPasswordCount,
                OTPTimeoutDuration = colApplicaitonConfiguration.OTPTimeoutDuration,
                TokenExpiryDuration = colApplicaitonConfiguration.TokenExpiryDuration,
                PreSessionTimeoutAlertDuration = colApplicaitonConfiguration.PreSessionTimeoutAlertDuration,
                SessionTimeoutDuration = colApplicaitonConfiguration.SessionTimeoutDuration
            };
        }
        #endregion

        #region Email Template Ids
        public void HydrateEmailTemplateIds()
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>
            { 
                { "CreateUser_UserEmailProvided", _configuration["EmailConfiguration:TemplateIds:CreateUser_UserEmailProvided"] }, 
                { "SendOTP_For_ForgotPassword", _configuration["EmailConfiguration:TemplateIds:SendOTP_For_ForgotPassword"] } 
   
            };

            ConcurrentDictionary<string, string> concurrentDictionary = new ConcurrentDictionary<string, string>(dictionary);
            _emailTemplateService.Set(concurrentDictionary);
        }
        #endregion
    }
}
