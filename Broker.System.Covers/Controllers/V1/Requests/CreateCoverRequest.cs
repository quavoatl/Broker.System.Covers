using System;
using Broker.System.Covers.Validation;

namespace Broker.System.Controllers.V1.Requests
{
    public class CreateCoverRequest
    {
        [SupportedCovers]
        public string Type { get; set; }
        [SupportedMultipliers]
        public double LimitMultiplier { get; set; }
    }
}