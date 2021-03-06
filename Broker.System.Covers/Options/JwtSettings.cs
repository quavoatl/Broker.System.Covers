using System;

namespace Broker.System.Covers.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}