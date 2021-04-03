using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Broker.System.Controllers.V1.Requests;
using Broker.System.Domain;
using NotImplementedException = System.NotImplementedException;

namespace Broker.System.Covers.Validation
{
    public class SupportedMultipliers : ValidationAttribute
    {
        public SupportedMultipliers()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateCoverRequest cover = (CreateCoverRequest) validationContext.ObjectInstance;

            if (cover.LimitMultiplier < 1 || cover.LimitMultiplier > 3)
            {
                return new ValidationResult(GetErrorMessage(cover.LimitMultiplier));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(double multiplier)
        {
            return multiplier + " not supported";
        }
    }
}