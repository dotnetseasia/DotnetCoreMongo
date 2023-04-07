using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.CommonMethods
{
    public class SecretGenerator
    {
        #region variable declaration
        public int MinimumLength { get; private set; }
        public int MaximumLength { get; private set; }
        public int MinimumLowerCaseChars { get; private set; }
        public int MinimumUpperCaseChars { get; private set; }
        public int MinimumNumericChars { get; private set; }
        public int MinimumSpecialChars { get; private set; }

        public static string AllLowerCaseChars { get; private set; }
        public static string AllUpperCaseChars { get; private set; }
        public static string AllNumericChars { get; private set; }
        public static string AllSpecialChars { get; private set; }
        private readonly string _allAvailableChars;

        private readonly RandomSecureVersion _randomSecure = new RandomSecureVersion();
        private int _minimumNumberOfChars;

        #endregion

        static SecretGenerator()
        {
            AllLowerCaseChars = GetCharRange('a', 'z');
            AllUpperCaseChars = GetCharRange('A', 'Z');
            AllNumericChars = GetCharRange('0', '9');
            AllSpecialChars = "!@#$";
        }

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <param name="maximumLength"></param>
        /// <param name="minimumLowerCaseChars"></param>
        /// <param name="minimumUpperCaseChars"></param>
        /// <param name="minimumNumericChars"></param>
        /// <param name="minimumSpecialChars"></param>
        public SecretGenerator(int minimumLength = 8, int maximumLength = 15, int minimumLowerCaseChars = 1,
                        int minimumUpperCaseChars = 1, int minimumNumericChars = 1, int minimumSpecialChars = 1)
        {

            _minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;

            if (minimumLength < _minimumNumberOfChars)
            {
                throw new ArgumentException(
                        "The minimum length of the string is smaller than the sum " +
                        "of the minimum characters of all catagories.",
                        "maximumLength");
            }

            MinimumLength = minimumLength;
            MaximumLength = maximumLength;

            MinimumLowerCaseChars = minimumLowerCaseChars;
            MinimumUpperCaseChars = minimumUpperCaseChars;
            MinimumNumericChars = minimumNumericChars;
            MinimumSpecialChars = minimumSpecialChars;

            _allAvailableChars =
            OnlyIfOneCharIsRequired(minimumLowerCaseChars, AllLowerCaseChars) +
            OnlyIfOneCharIsRequired(minimumUpperCaseChars, AllUpperCaseChars) +
            OnlyIfOneCharIsRequired(minimumNumericChars, AllNumericChars) +
            OnlyIfOneCharIsRequired(minimumSpecialChars, AllSpecialChars);
        }

        #region Generate random string
        public string Generate()
        {
            var length = _randomSecure.Next(MinimumLength, MaximumLength);

            // Get the required number of characters of each catagory and 
            // add random charactes of all catagories
            var minimumChars = GetRandomString(AllLowerCaseChars, MinimumLowerCaseChars) +
                        GetRandomString(AllUpperCaseChars, MinimumUpperCaseChars) +
                        GetRandomString(AllNumericChars, MinimumNumericChars) +
                        GetRandomString(AllSpecialChars, MinimumSpecialChars);
            var rest = GetRandomString(_allAvailableChars, length - minimumChars.Length);
            var unshuffeledResult = minimumChars + rest;

            // Shuffle the result so the order of the characters are unpredictable
            var result = unshuffeledResult.ShuffleTextSecure();
            return result;
        }

        private string OnlyIfOneCharIsRequired(int minimum, string allChars)
        {
            return minimum > 0 || _minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        private string GetRandomString(string possibleChars, int lenght)
        {
            var result = string.Empty;
            for (var position = 0; position < lenght; position++)
            {
                var index = _randomSecure.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }
            return result;
        }
        #endregion
    }
}
