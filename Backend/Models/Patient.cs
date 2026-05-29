
public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName {get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string? SSN { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Gender { get; set; }
    public string? TaxNumber { get; set; }
    public string? Religion { get; set; }
    public string? DriversLicense { get; set; }
    public string? MedicalInsuranceNumber { get; set; }

    public string? PasswordHash { get; set; }
    public bool IsRegistered { get; set; } = false;
    public DateTime? RegistrationDate { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}