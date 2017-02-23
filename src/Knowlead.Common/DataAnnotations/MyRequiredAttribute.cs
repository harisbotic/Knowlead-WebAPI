using System.ComponentModel.DataAnnotations;
using System;

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
            if(String.IsNullOrWhiteSpace(stringValue))
                return false;
                
            return true;
        }

        public override string FormatErrorMessage(string value)
        {   
            return Constants.ErrorCodes.RequiredField;
        }
    }
}
