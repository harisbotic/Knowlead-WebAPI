using System.Collections.Generic;

namespace Knowlead.DTO.SpecificModels {
    public class ResponseModel {
        public bool Success { get; set; }
        public Dictionary<string, List<ErrorModel>> ErrorMap { get; set; }
        public List<ErrorModel> ErrorList { get; set; }

        public ResponseModel(bool success) {
            Success = success;
        }

        public ResponseModel(bool success, Dictionary<string, List<ErrorModel>> errorMap) : this(success) {
            ErrorMap = errorMap;
        }

        public ResponseModel(bool success, List<ErrorModel> errorList) : this(success) {
            ErrorList = errorList;
        }

        public ResponseModel(bool success, ErrorModel error) : this(success) {
            ErrorList = new List<ErrorModel>();
            ErrorList.Add(error);
        }
    }
}