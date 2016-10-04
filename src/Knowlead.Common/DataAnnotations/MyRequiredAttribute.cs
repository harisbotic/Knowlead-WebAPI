using System.ComponentModel.DataAnnotations;

namespace Knowlead.Common.DataAnnotations
{
    public class MyRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string value)
        {   
            return Constants.ErrorCodes.RequiredField;
        }
    }
}
