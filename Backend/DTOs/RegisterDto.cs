
public class RegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string? SSN { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Gender { get; set; }
}