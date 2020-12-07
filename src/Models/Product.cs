using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int Stock { get; set; }

        public List<ProductImage> Image { get; set; }

        public List<ProductTag> Tags { get; set; }
    }
}