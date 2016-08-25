using Knowlead.Common;

namespace Knowlead.DTO {
    public class ErrorModel {
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public ErrorModel(string description, int? code = null) {
            ErrorDescription = description;
            ErrorCode = code;
        }
        public ErrorModel(string description, Constants.ErrorCodes code) : this(description, (int?)code) {
        }
    }
}