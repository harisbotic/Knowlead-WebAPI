using System.Collections.Generic;
using Knowlead.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Knowlead.DTO.ResponseModels {
    public class ResponseModel //TODO: rename to BasicResponse
    {
        public Dictionary<string, ICollection<string>> FormErrors { get; set; }
        public ICollection<string> Errors { get; set; }
        public object Object {get; set;}

        private void InitLists() 
        {
            FormErrors = new Dictionary<string, ICollection<string>>();
            Errors = new List<string>();
        }

        public ResponseModel()
        {
            InitLists();
        }

        public ResponseModel(FormErrorModel formError) : this()
        {
            AddFormError(formError);
        }

        public ResponseModel(ErrorModel error) : this()
        {
            AddError(error);
        }

        public ResponseModel(ModelStateDictionary modelState) : this()
        {   
            AddErrors(modelState);
        }

        public ResponseModel(IEnumerable<IdentityError> identityErrors) : this()
        {   
            AddErrors(identityErrors);
        }

        public void AddError(ErrorModel error)
        {
            Errors.Add($"{error.Value}{error.Param}");
        }

        public void AddFormError(FormErrorModel formError)
        {            
            ICollection<string> list;

            if (!FormErrors.TryGetValue(formError.Key, out list))
            {
                list = new List<string>();
                FormErrors.Add(formError.Key, list);
            }

            list.Add($"{@formError.Value}{formError.Param}");
        }

        public void AddErrors(ModelStateDictionary modelState)
        {
            foreach (var entry in modelState)
            {
                foreach (ModelError error in entry.Value.Errors)
                {
                    AddFormError(new FormErrorModel
                    {
                        Key = entry.Key,
                        Value = error.ErrorMessage
                    });
                }
            }
        }

        public void AddErrors(IEnumerable<IdentityError> identityErrors)
        {
            foreach (var error in identityErrors)
            {
                if(error.Code != null)
                    AddFormError(new FormErrorModel(error.Code, error.Description));
                else if (error.Description != null)
                    AddError(new ErrorModel(error.Description));
            }
        }
    }
}