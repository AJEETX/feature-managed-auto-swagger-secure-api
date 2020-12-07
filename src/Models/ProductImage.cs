using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Path { get; set; }
    }
}