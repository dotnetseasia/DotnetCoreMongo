using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class ColApplicaitonConfiguration
    {
        [BsonElement("PasswordExpiryDays")]
        public string PasswordExpiryDays { get; set; }
        public string PrePasswordExpiryNotificationDays { get; set; }
        public string MaxLoginAttempts { get; set; }
        public string UserLockedDuration { get; set; }
        public string RestrictedUsedPasswordCount { get; set; }
        public string OTPTimeoutDuration { get; set; }
        public string TokenExpiryDuration { get; set; }
        public string PreSessionTimeoutAlertDuration { get; set; }
        public string SessionTimeoutDuration { get; set; }
    }
}
