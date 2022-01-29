using System.ComponentModel.DataAnnotations;

namespace VinylStore.Web.ViewModels;

public class RegisterAdminViewModel
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!; 
    
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 10)]
    public string Password { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
    
    [Required]
    public string Country { get; set; } = null!;
    
    [Required]
    public string City { get; set; } = null!;
    
    [Required]
    public string AddressLine1 { get; set; } = null!;
    
    public string? AddressLine2 { get; set; }
}