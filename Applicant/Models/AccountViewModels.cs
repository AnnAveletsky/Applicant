using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicantWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "RememberInBrowserQ", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Account),
                  ErrorMessageResourceName = "PasswordLenghtError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]

        [Compare("Password",ErrorMessageResourceType = typeof (ApplicantWeb.App_LocalResources.Account),ErrorMessageResourceName="PasswordAndConfirmationDoNotMatch")]
        
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Account),
                  ErrorMessageResourceName = "PasswordLenghtError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]

        [Compare("Password", ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Account), ErrorMessageResourceName = "PasswordAndConfirmationDoNotMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress", ResourceType = typeof(ApplicantWeb.App_LocalResources.Account))]
        public string Email { get; set; }
    }
}
