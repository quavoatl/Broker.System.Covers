using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Broker.System.Domain
{
    public class Cover
    {
        [Key] public int CoverId { get; set; }
        public string BrokerId { get; set; }
        public string Type { get; set; }
        public double LimitMultiplier { get; set; }

        [ForeignKey(nameof(BrokerId))] 
        public IdentityUser User { get; set; }
        
    }
}