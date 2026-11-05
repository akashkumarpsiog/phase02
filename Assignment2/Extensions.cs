using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization; // namespace for language, culture and regional text conventions.

namespace Assignment2
{
    public static class Extensions
    {
        //String.ToTitleCase
        public static string? ToTitleCase(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            //CultureInfo represents the cultur associated with a particular language and region.
            //TextInfo provides information about the writing system associated with a particular culture, including casing rules, sorting rules, and other text-related information.
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo; //so we get the current culture and then the text info associated with that culture.
            return textInfo.ToTitleCase(input.ToLower());
            //we can call ToTitleCase on the TextInfo object to convert the input string to title case. We first convert the input string to lower case to ensure that all words are capitalized correctly.
            //We made it so instead of calling it on textinfo object we are calling it on string object. It adds convenince.
        }

        public static bool IsNullOrEmpty<T>(this List<T>? list)
        {
            return list == null || list.Count == 0; //null means the variable doesnt reference and actual object. a variable holds a reference to an object so if its not there its null. if the list exists (object exists) but doesnt have any elements, then count is zero.
        }

        private static readonly string[] Ones =
        {
        "Zero", "One", "Two", "Three", "Four",
        "Five", "Six", "Seven", "Eight", "Nine",
        "Ten", "Eleven", "Twelve", "Thirteen",
        "Fourteen", "Fifteen", "Sixteen",
        "Seventeen", "Eighteen", "Nineteen"
        };

        private static readonly string[] Tens =
        {
            "", "", "Twenty", "Thirty", "Forty",
            "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        public static string ToWords(this int number)
        {
            if (number < 0 || number > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be between 0 and 999");
            }
            if (number < 20)
            {
                return Ones[number];
            }

            if (number < 100)
            {
                int tens = number / 10;
                int ones = number % 10;

                if (ones == 0)
                {
                    return Tens[tens];
                }

                return Tens[tens] + "-" + Ones[ones].ToLower();
            }
            int hundreds = number / 100;
            int remainder = number % 100;
            if (remainder == 0)
            {
                return Ones[hundreds] + " Hundred";
            }
            return Ones[hundreds] + " Hundred and " + remainder.ToWords().ToLower();
        }

    }
}
