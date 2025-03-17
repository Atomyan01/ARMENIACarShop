using Microsoft.AspNetCore.Identity;

public class PersonModel : IdentityUser
{
	public string? PhoneNumber { get; set; }
}
