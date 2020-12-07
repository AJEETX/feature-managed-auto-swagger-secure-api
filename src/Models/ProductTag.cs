using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models
{
    public class ProductTag
    {
        [Required]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}