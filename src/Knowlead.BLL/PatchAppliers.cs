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
        public static void CustomApplyTo<T>(this JsonPatchDocument<T> patchDoc, T objectToApplyTo, Dictionary<string, Object> forManualPatch, ApplicationUser applicationUser = null) where T : class
        {
            if (objectToApplyTo == null)
            {
                throw new ArgumentNullException(nameof(objectToApplyTo));
            }

            var oa = new ObjectAdapter(patchDoc.ContractResolver, logErrorAction: null);

            foreach (var op in patchDoc.Operations)
            {
                var z = forManualPatch.Where(x => op.path.StartsWith($"/{x.Key}/", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if(z.Value != null)
                {
                    if(z.Value.GetType() == typeof(List<ApplicationUserLanguage>))
                        ApplyToApplicationUserLanguage(op, objectToApplyTo as ApplicationUserModel, (ICollection<ApplicationUserLanguage>)z.Value, applicationUser);
                    
                    if(z.Value.GetType() == typeof(List<ApplicationUserInterest>))
                        ApplyToApplicationUserInterests(op, objectToApplyTo as ApplicationUserModel, (ICollection<ApplicationUserInterest>)z.Value, applicationUser);
                    
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
                throw new ArgumentNullException(nameof(applicationUser));
            }

            var varName = nameof(ApplicationUserModel.Languages);
            var opPath = $"/{varName}/";

            if(op.OperationType == OperationType.Add)
            {
                var jObject = op.value as JObject;
                var languageId = jObject.GetValue(nameof(LanguageModel.CoreLookupId), StringComparison.CurrentCultureIgnoreCase).Value<int>();

                //Check if already exists
                if(langs.Where(x => x.ApplicationUserId == applicationUser.Id && x.LanguageId == languageId).FirstOrDefault() != null)
                    throw new Exception($"Language {languageId} Already added");

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
                var lang = langs.Where(x => x.LanguageId == id).FirstOrDefault();
                var langModel = applicationUserModel.Languages.Where(x => x.CoreLookupId == id).FirstOrDefault();

                if(lang == null || langModel == null)
                    throw new Exception("Language not found to be removed");

                langs.Remove(lang);
                applicationUserModel.Languages.Remove(langModel);
            }
        }

        // "ApplyTo" + name of class
        private static void ApplyToApplicationUserInterests(Operation op, ApplicationUserModel applicationUserModel, ICollection<ApplicationUserInterest> interests, ApplicationUser applicationUser) 
        {
            if(applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            var varName = nameof(ApplicationUserModel.Interests);
            var opPath = $"/{varName}/";

            if(op.OperationType == OperationType.Add)
            {
                var jObject = op.value as JObject;
                var fosId = jObject.GetValue(nameof(InterestModel.FosId), StringComparison.CurrentCultureIgnoreCase).Value<int>();
                var stars = jObject.GetValue(nameof(InterestModel.Stars), StringComparison.CurrentCultureIgnoreCase).Value<int>().LimitToRange(0,5);

                //Check if already exists
                if(interests.Where(x => x.ApplicationUserId == applicationUser.Id && x.FosId == fosId).FirstOrDefault() != null)
                    throw new Exception($"Interest {fosId} Already added");

                interests.Add(new ApplicationUserInterest
                {  
                    ApplicationUserId = applicationUser.Id,
                    FosId = fosId,
                    Stars = stars
                });
                applicationUserModel.Interests.Add(new InterestModel{FosId = fosId, Stars = stars});
            }

            else if(op.OperationType == OperationType.Replace)
            {
                var id = int.Parse((op.path.Substring(opPath.Length)));
                var jObject = op.value as JObject;
                var stars = jObject.GetValue(nameof(InterestModel.Stars), StringComparison.CurrentCultureIgnoreCase).Value<int>();

                var interest = interests.Where(x => x.FosId == id).FirstOrDefault();
                var interestModel = applicationUserModel.Interests.Where(x => x.FosId == id).FirstOrDefault();

                if(interest == null || interestModel == null)
                    throw new Exception("Interest not found to be removed");

                interest.Stars = stars;
                interestModel.Stars = stars;
            }

            else if(op.OperationType == OperationType.Remove)
            {
                var id = int.Parse((op.path.Substring(opPath.Length)));
                var interest = interests.Where(x => x.FosId == id).FirstOrDefault();
                var interestModel = applicationUserModel.Interests.Where(x => x.FosId == id).FirstOrDefault();

                if(interest == null || interestModel == null)
                    throw new Exception("Interest not found to be removed");

                interests.Remove(interest);
                applicationUserModel.Interests.Remove(interestModel);
            }
        }
    }
}