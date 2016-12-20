using System;
using Knowlead.DTO.ResponseModels;

namespace Knowlead.BLL.Exceptions
{
    public class FormErrorModelException: Exception
    {
        public FormErrorModel FormError;

        public FormErrorModelException(String formName, String errorMessage) : this(formName, errorMessage, null, null) { }

        public FormErrorModelException(String formName, String errorMessage, String param) : this(formName, errorMessage, param, null) { }

        public FormErrorModelException(String formName, String errorMessage, String param, Exception inner) : base(errorMessage, inner)
        {
            FormError = new FormErrorModel(formName, errorMessage, param);
        }
    }
}
