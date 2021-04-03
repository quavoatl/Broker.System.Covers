using System;

namespace Broker.System.Controllers.V1.Requests
{
    public class CreateCoverRequest
    {
        public string Type { get; set; }
        public double LimitMultiplier { get; set; }
    }
}