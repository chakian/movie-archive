using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MArchiveLibrary.Helpers
{
    public static class ParseHelper
    {
        #region Converters
        private static string CorrectDecimalSeparator(string input)
        {
            System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CurrentCulture;
            string decimalSeparator = currentCulture.NumberFormat.CurrencyDecimalSeparator;
            input = input.Replace(",", decimalSeparator).Replace(".", decimalSeparator);
            return input;
        }
        private static string CorrectDecimalSeparator(string part1, string part2)
        {
            return CorrectDecimalSeparator(part1 + "," + part2);
        }
        /// <summary>
        /// Gets two parts of a decimal number as string values and returns the decimal value
        /// </summary>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string part1, string part2)
        {
            string input = CorrectDecimalSeparator(part1, part2);
            decimal tempOut = 0;
            if (!decimal.TryParse(input, out tempOut))
                tempOut = 0;
            return tempOut;
        }
        /// <summary>
        /// Gets a string value containing a decimal number written with a ',' or '.' and returns a decent decimal object
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string input)
        {
            input = CorrectDecimalSeparator(input);
            decimal tempOut = 0;
            if (!decimal.TryParse(input, out tempOut))
                tempOut = 0;
            return tempOut;
        }
        /// <summary>
        /// Gets two parts of a double number as string values and returns the parsed double value
        /// </summary>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <returns></returns>
        public static double ToDouble(string part1, string part2)
        {
            string input = CorrectDecimalSeparator(part1, part2);
            double tempOut = 0;
            if (!double.TryParse(input, out tempOut))
                tempOut = 0;
            return tempOut;
        }
        /// <summary>
        /// Gets a string value containing a decimal number written with a ',' or '.' and returns a decent double object
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(string input)
        {
            input = CorrectDecimalSeparator(input);
            double tempOut = 0;
            if (!double.TryParse(input, out tempOut))
                tempOut = 0;
            return tempOut;
        }
        /// <summary>
        /// Converts a byte array to a string with UTF8 format
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }
        /// <summary>
        /// Converts a string to a byte array with UTF8 format
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        #endregion Converters
        #region Getters
        public static bool ToBool(object input)
        {
            bool returnValue;
            try
            {
                returnValue = bool.Parse(input.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Incorrect boolean value.", e);
            }
            return returnValue;
        }
        public static bool ToBool(object input, bool defaultValue)
        {
            bool returnValue;
            try { returnValue = bool.Parse(input.ToString()); } catch { returnValue = defaultValue; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as string and if the input is not convertible to string, returns null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToString(object input)
        {
            return ToString(input, null);
        }
        public static string ToString(object input, string defaultValue)
        {
            string returnValue;
            try { returnValue = input.ToString(); } catch { returnValue = defaultValue; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as regular integer. Returns 0 if input is not valid integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32(object input)
        {
            return ToInt32(input, 0);
        }
        public static int ToInt32(object input, int defaultValue)
        {
            int returnValue;
            try { returnValue = int.Parse(input.ToString()); } catch { returnValue = 0; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as 16 bit integer. Returns 0 if input is not valid integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static short ToInt16(object input)
        {
            short returnValue;
            try { returnValue = short.Parse(input.ToString()); } catch { returnValue = 0; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as 64 bit integer. Returns 0 if input is not valid integer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToInt64(object input)
        {
            long returnValue;
            try { returnValue = long.Parse(input.ToString()); } catch { returnValue = 0; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as double. Returns 0 if input is not valid number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(object input)
        {
            double returnValue;
            try { returnValue = double.Parse(input.ToString()); } catch { returnValue = 0; }
            return returnValue;
        }
        /// <summary>
        /// Just returns the value as regular DateTime. Returns new DateTime() if input is not valid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object input)
        {
            DateTime returnValue;
            try
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentUICulture;
                returnValue = DateTime.ParseExact(input.ToString(), provider.DateTimeFormat.ToString(), provider);
            }
            catch { returnValue = new DateTime(); }
            return returnValue;
        }
        #endregion Getters

        #region Validators
        public static bool isValidNumber(object input)
        {
            bool isValid = true;
            try
            {
                Regex rgx = new Regex(@"^[0-9]*$");
                if (!rgx.IsMatch(input.ToString()))
                    isValid = false;
            }
            catch { isValid = false; }
            return isValid;
        }
        #endregion Validators
    }
}
