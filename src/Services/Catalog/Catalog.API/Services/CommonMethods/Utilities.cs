using CompanyAdmin.API.Common;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.UploadDocument.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.CommonMethods
{
    public static class Utilities
    {
        public static string GenerateJsonWebToken(UserDetailsViewModel user, IList<string> roles, IConfiguration configuration, int expiryTime)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var crenditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var delimitedRoles = string.Join("|", roles);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        //new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                        //new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
                        new Claim(JwtRegisteredClaimNames.Typ, delimitedRoles), 
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryTime),
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var EncodeToken = tokenHandler.WriteToken(token);
                return EncodeToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string PasswordGenerator()
        {
            SecretGenerator secertGenerator = new SecretGenerator(minimumLength: 8,
                                              maximumLength: 15,
                                              minimumUpperCaseChars: 3,
                                              minimumLowerCaseChars: 1,
                                              minimumNumericChars: 1,
                                              minimumSpecialChars: 1);
            return secertGenerator.Generate();
        }

        public static string RandomStringGenerator(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string OTPGenerator(int length = 10)
        {
            SecretGenerator otpGenerator = new SecretGenerator(minimumLength: length,
                                             maximumLength: length,
                                             minimumUpperCaseChars: 0,
                                             minimumLowerCaseChars: 0,
                                             minimumNumericChars: length,
                                             minimumSpecialChars: 0);
            return otpGenerator.Generate();
        }
        /// <summary>
        /// Calculates age between DOB and To date.
        /// </summary>
        /// <param name="dOB">Date of birth</param>
        /// <param name="toDate">To Date</param>
        /// <returns></returns>
        public static string GetAge(DateTime dOB, DateTime toDate)
        {
            int age;
            age = toDate.Year - dOB.Year;

            if (toDate.DayOfYear < dOB.DayOfYear)
            {
                age -= 1;
            }
            if (age <= 0)
            {
                age = (toDate - dOB).Days;
                if (age >= 30)
                {
                    age /= 30;
                    return age > 1 ? $"{age} Months" : $"{age} Month";
                }
                return age > 1 ? $"{age} Days" : "1 Day";
            }
            return age > 1 ? $"{age} Years" : $"{age} Year";
        }


        /// <summary>
        /// Converting UTC to IST time zone
        /// </summary>
        /// <param name="currentDateUTC"></param>
        /// <returns></returns>
        public static DateTime ConvertUTCtoIST(DateTime currentDateUTC)
        {
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utc = currentDateUTC;
            return TimeZoneInfo.ConvertTimeFromUtc(utc, timezone);
        }
        public static string UploadDocument(UploadDocumentInputModel inputModel, string path)
        {
            string response = string.Empty;
            var sourceFilename = ContentDispositionHeaderValue
                          .Parse(inputModel.File.ContentDisposition)
                          .FileName
                          .Trim('"');
            string fileExtension = System.IO.Path.GetExtension(sourceFilename).ToLower();
            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".xlsx" || fileExtension == ".xls" || fileExtension == ".pdf" || fileExtension == ".png" || fileExtension == ".bmp" || fileExtension == ".docx" || fileExtension == ".doc")
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(sourceFilename) + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + System.IO.Path.GetExtension(sourceFilename);
                string targetPath = System.IO.Path.Combine(path, inputModel.FileFor, filename);
                if (!Directory.Exists(path + inputModel.FileFor.ToLower()))
                {
                    Directory.CreateDirectory(path + inputModel.FileFor.ToLower());
                    using (FileStream fs = System.IO.File.Create(targetPath))
                    {
                        inputModel.File.CopyTo(fs);
                        fs.Flush();
                        response = "success" ;
                    }
                }
                else
                {
                    using (FileStream fs = System.IO.File.Create(targetPath))
                    {
                        inputModel.File.CopyTo(fs);
                        fs.Flush();
                        response = "success";
                    }
                }
            }
            else
            {

                response = "Invalid file format";
            }
            return response;
        }            
    }
}
