using System.ComponentModel.DataAnnotations;

namespace Knowlead.Common.DataAnnotations
{
    public class MyRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            // only check string length if empty strings are not allowed
            var stringValue = value as string;
            if (stringValue != null) {
                return stringValue.Trim().Length != 0;
            }
                
            return true;
        }

        public override string FormatErrorMessage(string value)
        {   
            return Constants.ErrorCodes.RequiredField;
        }
    }
}
