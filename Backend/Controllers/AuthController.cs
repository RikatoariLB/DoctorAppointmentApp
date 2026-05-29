
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.Auth;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;
    
    public AuthController(ApplicationDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        if (await _context.Patients.AnyAsync(p => p.Email == dto.Email && p.IsRegistered))
            return BadRequest("Email already registered");
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            SSN = dto.SSN,
            Birthdate = dto.Birthdate,
            Gender = dto.Gender,
            PasswordHash = passwordHash,
            IsRegistered = true,
            RegistrationDate = DateTime.UtcNow
        };
        
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        
        var token = _jwtService.GenerateToken(patient.PatientId, patient.Email);
        
        return Ok(new AuthResponseDto
        {
            Token = token,
            PatientId = patient.PatientId,
            Email = patient.Email,
            FullName = $"{patient.FirstName} {patient.LastName}"
        });
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Email == dto.Email && p.IsRegistered);
        
        if (patient == null || patient.PasswordHash == null)
            return Unauthorized("Invalid credentials");
        
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, patient.PasswordHash))
            return Unauthorized("Invalid credentials");
        
        var token = _jwtService.GenerateToken(patient.PatientId, patient.Email);
        
        return Ok(new AuthResponseDto
        {
            Token = token,
            PatientId = patient.PatientId,
            Email = patient.Email,
            FullName = $"{patient.FirstName} {patient.LastName}"
        });
    }
}