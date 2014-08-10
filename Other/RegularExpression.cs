using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace iRobotKinect
{
    public class RegularExpression
    {
        // Function to test for Positive Integers. 
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive 
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        // Function to Test for Integers both Positive & Negative 
        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }

        // Function to Test for Positive Number both Integer & Real 
        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsFloatNumber(String strToCheck, float min = 0, float max = float.MaxValue)
        {
            float test;
            if (float.TryParse(strToCheck, out test))
            {
                // parsed OK, myString is a valid decimal
                if (test >= min && test <= max)
                {
                    // yay, it is positive!
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public static bool IsPositiveInteger(String strToCheck, int min = 0, int max = int.MaxValue)
        {
            int test;
            if (int.TryParse(strToCheck, out test))
            {
                // parsed OK, myString is a valid decimal
                if (test >= min && test <= max)
                {
                    // yay, it is positive!
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public static bool IsPositiveFloatNumber(String strToCheck)
        {
            float test;
            if(float.TryParse(strToCheck, out test))
            {
              // parsed OK, myString is a valid decimal
              if(test >= 0)
              {
                // yay, it is positive!
                  return false;
              }
            }
            return true;
            // +(?>\d++\.?+\d*+|\d*+\.?+\d++)
            // (?>(?>\d+)(?>\.?)(?>\d*)|(?>\d*)(?>\.?)(?>\d+))
            // ^[0-9]*([.][0-9])?+$
            // ([0-9]*\.)?[0-9]+            
            //Regex objNotFloatPattern = new Regex("^(?=.+)(?:[1-9]\d*|0)?(?:\.\d+)?$");
            //return !objNotFloatPattern.IsMatch(strToCheck);
        }

         // Function To test for valid US telephone Number. 
        //Description	
        //US Telephone Number where this is regular expression excludes the first number, after the area code,from being 0 or 1; it also allows an extension to be added where it does not have to be prefixed by 'x'.
        //Matches	
        //(910)456-7890 | (910)456-8970 x12 | (910)456-8970 1211
        //Non-Matches	
        //(910) 156-7890 | (910) 056-7890 | (910) 556-7890 x
        public static bool IsValidTelephoneNumber(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("^[\\(]{0,1}([0-9]){3}[\\)]{0,1}[ ]?([^0-1]){1}([0-9]){2}[ ]?[-]?[ ]?([0-9]){4}[ ]*((x){0,1}([0-9]){1,5}){0,1}$");
            return objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidName(String strToCheck)
        {
            // letters and space
            Regex objAlphaPattern = new Regex("^[a-zA-z ]{1,50}$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidZipCode(String strToCheck)
        {
            // 5 numbers
            Regex objAlphaPattern = new Regex("^[0-9]{5}$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidState(String strToCheck)
        {
            // 2 letter
            Regex objAlphaPattern = new Regex("^[A-Z]{2}$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidProject(String strToCheck)
        {
            // 3 Alphabetical characters
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-zA-Z]{3}$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidPlate(string plateFormat, String strToCheck)
        {
            // 3 Alphabetical characters
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-zA-Z0-9._-]{3,50}$");
            bool isMatch = objAlphaPattern.IsMatch(strToCheck);
            return isMatch;
        }

        public static bool IsValidPlate(String strToCheck)
        {
            // 3 Alphabetical characters
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-zA-Z0-9._-]{3,32}$");
            // NGE09042013 Regex objAlphaPattern = new Regex("^[a-zA-Z]{1}[0-9]{8}[.][a-zA-Z]{1}$"); // A00000000.A
            bool isMatch = objAlphaPattern.IsMatch(strToCheck);
            return isMatch;
        }

        public static bool IsValidSampleID(String strToCheck)
        {
            if (String.Equals(strToCheck, (string)"NTC", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(strToCheck, (string)"Positive", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(strToCheck, (string)"Pos", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(strToCheck, (string)"Neg", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(strToCheck, (string)"Negative", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // NGE08202013 bool isMatch;
            // NGE08202013 Regex objSampleWellPattern9Digits  = new Regex("^[a-zA-Z]{2}[0-9]{5}[.][0-9]{1}$"); // AA00000.1
            // NGE08202013 Regex objSampleWellPattern10Digits = new Regex("^[a-zA-Z]{2}[0-9]{5}[.][0-9]{1}[a-zA-Z]{1}$"); // AA00000.1D
            // NGE08202013 Regex objSampleWellPattern11Digits = new Regex("^[a-zA-Z]{2}[0-9]{5}[.][0-9]{1}[a-zA-Z]{2}$"); // AA00000.1DD
            // NGE08202013 isMatch = objSampleWellPattern9Digits.IsMatch(strToCheck) || objSampleWellPattern10Digits.IsMatch(strToCheck) || objSampleWellPattern11Digits.IsMatch(strToCheck);
            //return isMatch;
            Regex objAlphaPattern = new Regex("^[a-zA-Z0-9._-]{3,32}$");
            return objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidPlateType(String strToCheck)
        {
            // 3 Alphabetical characters
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-zA-Z]{2}$");
            return objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsValidPlateTypeName(String strToCheck)
        {
            // 3 Alphabetical characters
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-zA-Z-_]{3,32}$");
            return objAlphaPattern.IsMatch(strToCheck);
        }

        // Function To test for valid email 
        //  ^                # Start of the line
        //  [a-z0-9_-]	     # Match characters and symbols in the list, a-z, 0-9, underscore, hyphen
        //  {3,15}           # Length at least 3 characters and maximum length of 15 
        //  $                # End of the line
        public static bool IsValidUserName(String strToCheck)
        {
            // Regex objAlphaPattern = new Regex("^[a-zA-Z](.{1,9})$");
            Regex objAlphaPattern = new Regex("^[a-z0-9_-]{3,15}$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Determines whether the username meets conditions.
        /// Username conditions:
        /// Must be 1 to 24 character in length
        /// Must start with letter a-zA-Z
        /// May contain letters, numbers or '.','-' or '_'
        /// Must not end in '.','-','._' or '-_' 
        /// </summary>
        /// <param name="userName">proposed username</param>
        /// <returns>True if the username is valid</returns>
         public static bool IsValidUser(String strToCheck)
         {
        Regex IsUserNameAllowedRegEx = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$", RegexOptions.Compiled);
        Regex IsUserNameIllegalEndingRegEx = new Regex(@"(\.|\-|\._|\-_)$", RegexOptions.Compiled);
        if (string.IsNullOrEmpty(strToCheck)
                || !IsUserNameAllowedRegEx.IsMatch(strToCheck)
                || IsUserNameIllegalEndingRegEx.IsMatch(strToCheck)
                //|| ProfanityFilter.IsOffensive(strToCheck)
            )
            {
                return false;
            }
            return true;
        }
        
         // Function To test for valid password
        //  (			# Start of group
        //  (?=.*\d)		#   must contains one digit from 0-9
        //  (?=.*[a-z])		#   must contains one lowercase characters
        //  (?=.*[A-Z])		#   must contains one uppercase characters
        //  (?=.*[@#$%])	#   must contains one special symbols in the list "@#$%"
        //    .	            #   match anything with previous condition checking
        //   {6,20}	        #   length at least 6 characters and maximum of 20	
        //)			# End of group
        public static bool IsValidUserPassword(String strToCheck)
        {
            // "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})"
            // Regex objAlphaPattern = new Regex("((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})");
            Regex objAlphaPattern = new Regex("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        // Function To test for valid email 
        public static bool IsValidEmail(String strToCheck)
        {            
            // Regex objAlphaPattern = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            // Regex objAlphaPattern = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            // Regex objAlphaPattern = new Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            //Regex objAlphaPattern = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            const String pattern =
   @"^([0-9a-zA-Z]" + //Start with a digit or alphabate
   @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continues or ending +-_. chars in email
   @")+" +
   @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

            Regex expression = new Regex(pattern);

            return expression.IsMatch(strToCheck);
        }

        // Function To test for Alphabets. 
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        public static bool IsWellLetterA_thur_H(String strToCheck)
        {
            // Regex objSampleWellPattern = new Regex(@"([a-hA-H]((0[1-9]|1[012])|[1-9]))");
            Regex objSampleWellPattern = new Regex("[a-hA-H]");
            return !objSampleWellPattern.IsMatch(strToCheck);
        }

        public static bool IsWellNumber01_thur_12(String strToCheck)
        {
            Regex objSampleWellPattern = new Regex("((0[1-9]|1[012])|[1-9])");
            return !objSampleWellPattern.IsMatch(strToCheck);
        }

        public static bool IsNumber1_thur_9(String strToCheck)
        {
            Regex objSampleWellPattern = new Regex("[1-9]");
            return !objSampleWellPattern.IsMatch(strToCheck);
        }

        public static bool IsWellNumber1_thur_12(String strToCheck)
        {
            Regex objSampleWellPattern = new Regex("((0[1-9]|1[012])|[1-9])");
            return !objSampleWellPattern.IsMatch(strToCheck);
        }

        public static bool IsSampleWell1Or2DigitNumber(String strToCheck)
        {
            Regex objSampleWellPattern = new Regex("([a-hA-H]((0[1-9]|1[012])|[1-9]))");
            return objSampleWellPattern.IsMatch(strToCheck);
        }
        
        public static bool IsSampleWell2DigitNumberOnly(String strToCheck)
        {
            // Regex objSampleWellPattern = new Regex("[a-zA-Z][0-12]");
            // Regex objSampleWellPattern = new Regex(@"[a-hA-H][1][012]|[0][1-9]"); // Good!!!
            Regex objSampleWellPattern = new Regex("([a-hA-H](0[1-9]|1[012]))");
            return objSampleWellPattern.IsMatch(strToCheck);

           /* Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
            */
            // Date pattern example http://www.regular-expressions.info/dates.html
            /*
             * sub isvaliddate {
              my $input = shift;
              if ($input =~ m!^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$!) {
                # At this point, $1 holds the year, $2 the month and $3 the day of the date entered
                if ($3 == 31 and ($2 == 4 or $2 == 6 or $2 == 9 or $2 == 11)) {
                  return 0; # 31st of a month with 30 days
                } elsif ($3 >= 30 and $2 == 2) {
                  return 0; # February 30th or 31st
                } elsif ($2 == 2 and $3 == 29 and not ($1 % 4 == 0 and ($1 % 100 != 0 or $1 % 400 == 0))) {
                  return 0; # February 29th outside a leap year
                } else {
                  return 1; # Valid date
                }
              } else {
                return 0; # Not a date
              }
            }
            */
        }

    }
}
