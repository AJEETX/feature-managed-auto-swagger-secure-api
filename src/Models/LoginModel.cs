using System.ComponentModel.DataAnnotations;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}