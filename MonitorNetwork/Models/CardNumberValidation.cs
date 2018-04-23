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

            try
            {
                float cardNumber = Int64.Parse(cardNumberString);

                if(cardNumber < 1000000000000000 || cardNumber > 9999999999999999)
                {
                    return false;
                }

                return true;

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