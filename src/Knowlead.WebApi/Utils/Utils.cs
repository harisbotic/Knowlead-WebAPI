using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Knowlead.Utils
{
    static class Utils {
        public static string ConcatToString<T>(this IEnumerable<T> enumerable, string delim = " ") 
        {
            string ret = "";
            foreach (T t in enumerable) {
                ret += t.ToString() + delim;
            }
            ret.Remove(ret.Length - delim.Length);
            return ret;
        }

        public static List<string> AsStringList<T>(this IEnumerable<T> enumerable) 
        {
            List<string> ret = new List<string>();
            foreach (T t in enumerable) {
                ret.Add(t.ToString());
            }
            return ret;
        } 

        public static Dictionary<string, List<string>>AsDictionary(this ModelStateDictionary state) 
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            foreach (var tmp in state) 
            {
                ret[tmp.Key] = new List<string>();
                foreach (var error in tmp.Value.Errors) {
                    ret[tmp.Key].Add(error.ErrorMessage);
                }
            }
            return ret;
        }
    }
}