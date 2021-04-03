using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Broker.System.Covers.Validation;
using Microsoft.AspNetCore.Identity;

namespace Broker.System.Domain
{
    public class Cover
    {
        [Key] public int CoverId { get; set; }
        public string BrokerId { get; set; }
        [SupportedCovers]
        public string Type { get; set; }
        public double LimitMultiplier { get; set; }

        [ForeignKey(nameof(BrokerId))] 
        public IdentityUser User { get; set; }
        
    }
}