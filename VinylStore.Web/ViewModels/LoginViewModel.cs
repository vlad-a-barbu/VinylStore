using System.ComponentModel.DataAnnotations;

namespace VinylStore.Web.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!; 
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
