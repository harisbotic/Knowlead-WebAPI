using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using static Knowlead.Common.Constants;

namespace Knowlead.DTO.Mappers {
    static class ErrorMappers {
        public static Dictionary<string, List<ErrorModel>> MapToErrorDictionary(this Dictionary<string, List<string>> dictionary, int? code = null) {
            Dictionary<string, List<ErrorModel>> ret = new Dictionary<string, List<ErrorModel>>();
            foreach (var entry in dictionary) {
                ret.Add(entry.Key, entry.Value.MapToErrorList(code));
            }
            return ret;
        }

        public static List<ErrorModel> MapToErrorList(this List<string> list, int? code = null) {
            List<ErrorModel> ret = new List<ErrorModel>();
            foreach (var val in list) {
                ret.Add(new ErrorModel(val, code));
            }
            return ret;
        }

        public static List<ErrorModel> MapToErrorList(this IEnumerable<IdentityError> errors) {
            return errors.Select(err => new ErrorModel(err.Description, ErrorCodes.IdentityError)).ToList();
        }
    }
}