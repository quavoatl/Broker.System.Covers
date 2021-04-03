using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Broker.System.Controllers.V1.Requests;
using Broker.System.Domain;
using NotImplementedException = System.NotImplementedException;

namespace Broker.System.Covers.Validation
{
    public class SupportedCoversAttribute : ValidationAttribute
    {
        private List<string> _supportedCovers = new List<string>() {"accidents", "theft", "naturalhazards"};

        public SupportedCoversAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateCoverRequest cover = (CreateCoverRequest) validationContext.ObjectInstance;

            if (!_supportedCovers.Contains(cover.Type))
            {
                return new ValidationResult(GetErrorMessage(cover.Type));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(string coverType)
        {
            return coverType + " currently not supported";
        }
    }
}