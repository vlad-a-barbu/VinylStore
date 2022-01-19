using System.ComponentModel.DataAnnotations;

namespace VinylStore.Web.ViewModels;

public class RegisterViewModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [EmailAddress]
    public string Email { get; set; } = null!; 
    
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 10)]
    public string Password { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
    
    [Required]
    public string Country { get; set; } = null!;
    
    [Required]
    public string City { get; set; } = null!;
    
    [Required]
    public string AddressLine1 { get; set; } = null!;
    
    public string? AddressLine2 { get; set; }
}
