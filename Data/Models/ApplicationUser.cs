using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace My_Books.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="Full name fieled is required")]
        public string FullName { get; set; }
        
    }
}
