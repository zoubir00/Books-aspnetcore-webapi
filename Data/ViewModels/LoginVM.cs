using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace My_Books.Data.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is required")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
       [StringLength(50,MinimumLength =5)]
        public string Password { get; set; }
    }
}
