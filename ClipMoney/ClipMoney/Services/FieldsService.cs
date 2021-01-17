using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ClipMoney.Services
{
    public class FieldsService
    {
        public Dictionary<string, string> ValidateModel(ModelStateDictionary ModelState)
        {
            var oErrorsDictionary = new Dictionary<string, string>();
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        var aErrors = error.ErrorMessage.Split(',');
                        oErrorsDictionary.Add(aErrors[0], aErrors[1]);
                    }
                }
            }

            return oErrorsDictionary;
        }
    }
}