namespace VinylStore.DataObjects.AuthenticationModels;

public class RegisterUser
{
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
  
    public string ConfirmPassword { get; set; } = null!;
    
    public string Country { get; set; } = null!;
    
    public string City { get; set; } = null!;
 
    public string AddressLine1 { get; set; } = null!;
    
    public string? AddressLine2 { get; set; }
}
