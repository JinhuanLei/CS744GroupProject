using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MonitorNetwork.Models
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    public class CardNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object creditCardNumber)
        {
            //// check whether input string is null or empty
            if (creditCardNumber == null || string.IsNullOrEmpty(creditCardNumber.ToString()))
            {
                return false;
            }

            string cardNumberString = creditCardNumber.ToString();

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            try
            {
                float cardNumber = Int64.Parse(cardNumberString);

                if(cardNumber < 1000000000000000 || cardNumber > 9999999999999999)
                {
                    return false;
                }

                int sumOfDigits = cardNumberString.Where((e) => e >= '0' && e <= '9')
                                .Reverse()
                                .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                                .Sum((e) => e / 10 + e % 10);


                //// If the final sum is divisible by 10, then the credit card number
                //   is valid. If it is not divisible by 10, the number is invalid.            
                return sumOfDigits % 10 == 0;
            } catch (Exception)
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}