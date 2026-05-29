
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.DTOs.Appointments;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment(CreateAppointmentDto dto)
    {
        int patientId;

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        
        if (userIdClaim != null)
        {
            patientId = int.Parse(userIdClaim.Value);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || 
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Phone))
            {
                return BadRequest("Guest users must provide FirstName, LastName, Email, and Phone");
            }
            
            var guestPatient = new Patient
            {
                FirstName = dto.FirstName!,
                LastName = dto.LastName!,
                Email = dto.Email!,
                Phone = dto.Phone!,
                IsRegistered = false
            };
            _context.Patients.Add(guestPatient);
            await _context.SaveChangesAsync();
            patientId = guestPatient.PatientId;
        }
        
        var conflict = await _context.Appointments.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.ClinicId == dto.ClinicId &&
            a.AppointmentDate.Date == dto.AppointmentDate.Date &&
            a.TimeSlot == dto.TimeSlot &&
            a.Status != "Cancelled");
        
        if (conflict)
            return Conflict("This time slot is already booked");

        var appointment = new Appointment
        {
            PatientId = patientId,
            DoctorId = dto.DoctorId,
            ClinicId = dto.ClinicId,
            CategoryId = dto.CategoryId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,
            Notes = dto.Notes,
            Status = "Scheduled"
        };
        
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        await _context.Entry(appointment)
            .Reference(a => a.Patient)
            .LoadAsync();
        await _context.Entry(appointment)
            .Reference(a => a.Doctor)
            .LoadAsync();
        await _context.Entry(appointment)
            .Reference(a => a.Clinic)
            .LoadAsync();
        
        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, appointment);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Clinic)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);
        
        if (appointment == null)
            return NotFound();
        
        return appointment;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetMyAppointments()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var patientId = int.Parse(userIdClaim.Value);
        
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Clinic)
            .Include(a => a.Category)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
        
        return Ok(appointments);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAppointment(int id, UpdateAppointmentDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var patientId = int.Parse(userIdClaim.Value);
        
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound();

        if (appointment.PatientId != patientId)
            return Forbid();

        if (dto.AppointmentDate != appointment.AppointmentDate || dto.TimeSlot != appointment.TimeSlot)
        {
            var conflict = await _context.Appointments.AnyAsync(a =>
                a.AppointmentId != id &&
                a.DoctorId == appointment.DoctorId &&
                a.ClinicId == appointment.ClinicId &&
                a.AppointmentDate.Date == dto.AppointmentDate.Date &&
                a.TimeSlot == dto.TimeSlot &&
                a.Status != "Cancelled");
            
            if (conflict)
                return Conflict("This time slot is already booked");
        }
        
        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.TimeSlot = dto.TimeSlot;
        appointment.Notes = dto.Notes;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var patientId = int.Parse(userIdClaim.Value);
        
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound();

        if (appointment.PatientId != patientId)
            return Forbid();

        appointment.Status = "Cancelled";
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}