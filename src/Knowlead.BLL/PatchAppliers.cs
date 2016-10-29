using System;
using System.Collections.Generic;
using System.Linq;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.UserModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Linq;

namespace Knowlead.BLL
{
    public static class PatchAppliers
    {
        public static void CustomApplyTo<T>(this JsonPatchDocument<T> patchDoc, T objectToApplyTo, Dictionary<string, Object> vars, ApplicationUser applicationUser = null) where T : class
        {
            if (objectToApplyTo == null)
            {
                throw new ArgumentNullException(nameof(objectToApplyTo));
            }

            var oa = new ObjectAdapter(patchDoc.ContractResolver, logErrorAction: null);

            foreach (var op in patchDoc.Operations)
            {
                var z = vars.Where(x => op.path.StartsWith($"/{x.Key}/", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if(z.Value != null)
                {
                    if(z.Value.GetType() == typeof(List<ApplicationUserLanguage>))
                        ApplyToApplicationUserLanguage(op, objectToApplyTo as ApplicationUserModel, (ICollection<ApplicationUserLanguage>)z.Value, applicationUser);
                    

                }
                else 
                {
                    op.Apply(objectToApplyTo, oa);
                }
            }
        }
        // "ApplyTo" + name of class
        private static void ApplyToApplicationUserLanguage(Operation op, ApplicationUserModel applicationUserModel, ICollection<ApplicationUserLanguage> langs, ApplicationUser applicationUser) 
        {
            if(applicationUser == null)
            {
                //TODO: Make a custom exception and throw/catch that
                throw new ArgumentNullException(nameof(applicationUser));
            }

            var varName = nameof(ApplicationUserModel.Languages);
            var opPath = $"/{varName}/";

            if(op.OperationType == OperationType.Add)
            {
                var @value = op.value as JObject;
                var languageId = (int)(@value.GetValue(nameof(LanguageModel.CoreLookupId), StringComparison.CurrentCultureIgnoreCase));

                //Check if already exists
                if(langs.Where(x => x.ApplicationUserId == applicationUser.Id && x.LanguageId == languageId).FirstOrDefault() != null)
                    throw new Exception("Item Already exists");

                langs.Add(new ApplicationUserLanguage
                {
                    LanguageId = languageId,  
                    ApplicationUserId = applicationUser.Id
                });
                applicationUserModel.Languages.Add(new LanguageModel{CoreLookupId = languageId});
            }

            else if(op.OperationType == OperationType.Remove)
            {
                var id = int.Parse((op.path.Substring(opPath.Length)));
                var lang = langs.Where(x => x.LanguageId == id).SingleOrDefault();
                var langModel = applicationUserModel.Languages.Where(x => x.CoreLookupId == id).SingleOrDefault();

                if(lang == null || langModel == null)
                    throw new Exception("Language not found to be removed");

                langs.Remove(lang);
                applicationUserModel.Languages.Remove(langModel);
            }
        }
    }
}