using System.ComponentModel.DataAnnotations;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
    public class LoginRequest
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}