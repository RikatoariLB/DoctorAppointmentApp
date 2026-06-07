
namespace Backend.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var hasFullData = context.Specialities.Count() >= 6 
                       && context.Clinics.Count() >= 4 
                       && context.Categories.Count() >= 5 
                       && context.Doctors.Count() >= 8;
        
        if (hasFullData)
        {
            Console.WriteLine("✅ Database already has complete seed data.");
            return;
        }

        Console.WriteLine("🌱 Seeding database with test data...");

        context.Doctors.RemoveRange(context.Doctors);
        context.Appointments.RemoveRange(context.Appointments);
        context.Specialities.RemoveRange(context.Specialities);
        context.Clinics.RemoveRange(context.Clinics);
        context.Categories.RemoveRange(context.Categories);
        await context.SaveChangesAsync();
        Console.WriteLine("🗑️  Cleared existing partial data");

        var specialities = new[]
        {
            new Speciality { Name = "Cardiology" },
            new Speciality { Name = "Dermatology" },
            new Speciality { Name = "Neurology" },
            new Speciality { Name = "Pediatrics" },
            new Speciality { Name = "Orthopedics" },
            new Speciality { Name = "General Practice" }
        };
        context.Specialities.AddRange(specialities);
        await context.SaveChangesAsync();

        var clinics = new[]
        {
            new Clinic 
            { 
                Name = "City Medical Center", 
                Address = "123 Main St, Downtown", 
                Phone = "+1-555-0101" 
            },
            new Clinic 
            { 
                Name = "Sunrise Health Clinic", 
                Address = "456 Oak Ave, Westside", 
                Phone = "+1-555-0202" 
            },
            new Clinic 
            { 
                Name = "Green Valley Hospital", 
                Address = "789 Pine Rd, North District", 
                Phone = "+1-555-0303" 
            },
            new Clinic 
            { 
                Name = "Metro Care Center", 
                Address = "321 Elm Blvd, East End", 
                Phone = "+1-555-0404" 
            }
        };
        context.Clinics.AddRange(clinics);
        await context.SaveChangesAsync();

        var categories = new[]
        {
            new Category 
            { 
                Name = "Consultation", 
                Description = "Initial medical consultation" 
            },
            new Category 
            { 
                Name = "Follow-up", 
                Description = "Follow-up appointment for existing patients" 
            },
            new Category 
            { 
                Name = "Emergency", 
                Description = "Urgent medical care" 
            },
            new Category 
            { 
                Name = "Routine Checkup", 
                Description = "Regular health checkup" 
            },
            new Category 
            { 
                Name = "Specialist Visit", 
                Description = "Visit to medical specialist" 
            }
        };
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        var doctors = new[]
        {
            new Doctor 
            { 
                Name = "Dr. Sarah Johnson", 
                Email = "sarah.johnson@citymedical.com",
                SpecialityId = specialities[0].SpecialityId, 
                ClinicId = clinics[0].ClinicId 
            },
            new Doctor 
            { 
                Name = "Dr. Michael Chen", 
                Email = "michael.chen@citymedical.com",
                SpecialityId = specialities[2].SpecialityId, 
                ClinicId = clinics[0].ClinicId
            },
            new Doctor 
            { 
                Name = "Dr. Emily Rodriguez", 
                Email = "emily.rodriguez@sunrise.com",
                SpecialityId = specialities[3].SpecialityId, 
                ClinicId = clinics[1].ClinicId 
            },
            new Doctor 
            { 
                Name = "Dr. James Wilson", 
                Email = "james.wilson@sunrise.com",
                SpecialityId = specialities[1].SpecialityId,
                ClinicId = clinics[1].ClinicId
            },
            new Doctor 
            { 
                Name = "Dr. Aisha Patel", 
                Email = "aisha.patel@greenvalley.com",
                SpecialityId = specialities[4].SpecialityId, 
                ClinicId = clinics[2].ClinicId 
            },
            new Doctor 
            { 
                Name = "Dr. Robert Martinez", 
                Email = "robert.martinez@greenvalley.com",
                SpecialityId = specialities[5].SpecialityId,
                ClinicId = clinics[2].ClinicId
            },
            new Doctor 
            { 
                Name = "Dr. Lisa Anderson", 
                Email = "lisa.anderson@metrocare.com",
                SpecialityId = specialities[0].SpecialityId, 
                ClinicId = clinics[3].ClinicId 
            },
            new Doctor 
            { 
                Name = "Dr. David Kim", 
                Email = "david.kim@metrocare.com",
                SpecialityId = specialities[3].SpecialityId,
                ClinicId = clinics[3].ClinicId
            }
        };
        context.Doctors.AddRange(doctors);
        await context.SaveChangesAsync();

        Console.WriteLine($"✅ Seeded: {specialities.Length} specialities, {clinics.Length} clinics, {categories.Length} categories, {doctors.Length} doctors");
    }
}