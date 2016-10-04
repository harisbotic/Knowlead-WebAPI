using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Knowlead.Common.DataAnnotations
{
    public sealed class MyEmailAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(),
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9]" +
                @"(?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
                RegexOptions.IgnoreCase);
        }

        public override string FormatErrorMessage(string value)
        {
            return Constants.ErrorCodes.EmailInvalid;
        }
    }
}