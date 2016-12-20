using System;
using Knowlead.DTO.ResponseModels;

namespace Knowlead.BLL.Exceptions
{
    public class ErrorModelException : Exception
    {
        public ErrorModel Error;

        public ErrorModelException(String errorMessage) : this(errorMessage, null, null) { }

        public ErrorModelException(String errorMessage, String param) : this(errorMessage, param, null) { }

        public ErrorModelException(String errorMessage, String param, Exception inner) : base(errorMessage, inner)
        {
            Error = new ErrorModel(errorMessage, param);
        }
    }
}
