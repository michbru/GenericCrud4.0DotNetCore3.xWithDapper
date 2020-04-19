using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCrudApiDapper.Models
{
    public class ModelValidation
    {

        public List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        public List<ValidationResult> ValidateModelCustom(dynamic model, ValidationContext validationContext)
        {
          //  var validationResults = new List<ValidationResult>();

            //  model.Validate(model);
            var ctx = new ValidationContext(model, null, null);
            var errs = model.Validate(ctx);
            //var ctx = new ValidationContext(model, null, null);
            //model.Validate(validationContext);
            // Validator.TryValidateObject(model, ctx, validationResults, true);
            return errs;
        }

        public List<ValidationResult> ValidateModelAndCustomValidation(dynamic typedEntityRecord, ValidationContext validationContext, Type type)
        {
          //  var validationResults = new List<ValidationResult>();
            var errs = ValidateModel(validationContext.ObjectInstance);
            if (errs.Count > 0) 
            {
              //  var test = "erros";
                return errs; 
            
            };
            if (type.GetMethod("Validate") != null)
            {
                errs = ValidateModelCustom(typedEntityRecord, validationContext);
            }

          
         //   var validationResults = new List<ValidationResult>();
         //   var ctx = new ValidationContext(model, null, null);
         //   Validator.TryValidateObject(model, ctx, validationResults, true);
            return errs;
        }

    }
    
}
