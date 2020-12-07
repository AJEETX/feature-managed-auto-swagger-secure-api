using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public string TagDescription { get; set; }

        public List<ProductTag> Products { get; set; }
    }
}