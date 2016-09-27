using static Knowlead.Common.Constants;

namespace Knowlead.DTO.SpecificModels {
    public class ErrorModel {
        public int? ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public ErrorModel(string description, int? code = null) {
            ErrorDescription = description;
            ErrorCode = code;
        }
        public ErrorModel(string description, ErrorCodes code) : this(description, (int?)code) {
        }
    }
}