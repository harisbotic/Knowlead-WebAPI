using System.Collections.Generic;

namespace Knowlead.DTO {
    public class ResponseModel {
        public bool Success { get; set; }
        public Dictionary<string, List<ErrorModel>> ErrorMap { get; set; }

        public List<ErrorModel> ErrorList { get; set; }
    }
}