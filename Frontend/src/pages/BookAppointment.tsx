
import { useState, useEffect } from 'react';
import { doctorService } from '@/services/doctorService';
import { appointmentService } from '@/services/appointmentService';
import { authService } from '@/services/authService';
import { Doctor, Clinic, Category } from '@/types';
import { Loading } from '@/components/common/Loading';

export const BookAppointment = () => {
  const [loading, setLoading] = useState(false);
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [clinics, setClinics] = useState<Clinic[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  
  const isAuthenticated = authService.isAuthenticated();
  
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    birthdate: '',
    phone: '',
    doctorId: '',
    clinicId: '',
    categoryId: '',
    appointmentDate: '',
    appointmentTime: '',
    durationMinutes: '',
    notes: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    setLoading(true);
    try {
      const [doctorsData, clinicsData, categoriesData] = await Promise.all([
        doctorService.getAll(),
        doctorService.getClinics(),
        doctorService.getCategories()
      ]);
      setDoctors(doctorsData);
      setClinics(clinicsData);
      setCategories(categoriesData);
    } catch (err: any) {
      setError(err.response?.data || 'Failed to load data');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    if (!isAuthenticated && (!formData.firstName || !formData.lastName || !formData.email || !formData.phone || !formData.birthdate)) {
      setError('Guest users must provide first name, last name, email, phone, and date of birth');
      return;
    }

    if (!formData.doctorId || !formData.appointmentDate || !formData.appointmentTime) {
      setError('Please fill in all required fields');
      return;
    }

    setLoading(true);
    try {
      const appointmentDateTime = new Date(`${formData.appointmentDate}T${formData.appointmentTime}`);
      const timeSlot = formData.appointmentTime + ':00';

      await appointmentService.create({
        firstName: isAuthenticated ? undefined : formData.firstName,
        lastName: isAuthenticated ? undefined : formData.lastName,
        email: isAuthenticated ? undefined : formData.email,
        phone: isAuthenticated ? undefined : formData.phone,
        birthdate: isAuthenticated ? undefined : new Date(formData.birthdate),
        doctorId: parseInt(formData.doctorId),
        clinicId: parseInt(formData.clinicId),
        categoryId: parseInt(formData.categoryId),
        appointmentDate: appointmentDateTime,
        timeSlot: timeSlot,
        durationMinutes: parseInt(formData.durationMinutes),
        notes: formData.notes
      });

      setSuccess('Appointment booked successfully!');
      setFormData({
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        birthdate: '',
        doctorId: '',
        clinicId: '',
        categoryId: '',
        appointmentDate: '',
        appointmentTime: '',
        durationMinutes: '',
        notes: ''
      });
    } catch (err: any) {
      const errorMessage = err.response?.data?.message || err.response?.data || 'Failed to book appointment';
      setError(typeof errorMessage === 'string' ? errorMessage : JSON.stringify(errorMessage));
    } finally {
      setLoading(false);
    }
  };

  const selectedDoctor = doctors.find(d => d.doctorId === parseInt(formData.doctorId));
  const availableClinics = selectedDoctor ? [selectedDoctor.clinic] : clinics;

  if (loading && doctors.length === 0) {
    return <Loading />;
  }

  return (
    <div className="max-w-3xl mx-auto">
      <div className="bg-white rounded-lg shadow-lg p-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-2">Book an Appointment</h1>
        <p className="text-gray-600 mb-6">Schedule your medical appointment</p>

        {error && (
          <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 mb-6 rounded">
            {error}
          </div>
        )}
        
        {success && (
          <div className="bg-green-100 border-l-4 border-green-500 text-green-700 p-4 mb-6 rounded">
            {success}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-6">
          {!isAuthenticated && (
            <>
              <h3 className="text-xl font-semibold text-gray-700 border-b pb-2">Patient Information</h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label htmlFor="firstName" className="block text-sm font-medium text-gray-700 mb-2">
                    First Name *
                  </label>
                  <input
                    type="text"
                    id="firstName"
                    name="firstName"
                    value={formData.firstName}
                    onChange={handleChange}
                    required={!isAuthenticated}
                    className="form-input"
                  />
                </div>
                <div>
                  <label htmlFor="lastName" className="block text-sm font-medium text-gray-700 mb-2">
                    Last Name *
                  </label>
                  <input
                    type="text"
                    id="lastName"
                    name="lastName"
                    value={formData.lastName}
                    onChange={handleChange}
                    required={!isAuthenticated}
                    className="form-input"
                  />
                </div>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div>
                  <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-2">
                    Email *
                  </label>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required={!isAuthenticated}
                    className="form-input"
                  />
                </div>
                <div>
                  <label htmlFor="phone" className="block text-sm font-medium text-gray-700 mb-2">
                    Phone *
                  </label>
                  <input
                    type="tel"
                    id="phone"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                    required={!isAuthenticated}
                    className="form-input"
                  />
                </div>
              </div>                
              <div>
                <label htmlFor="birthdate" className='block text-sm font-medium text-gray-700 mb-2'>
                  Date of Birth *
                </label>
                <input 
                  type="date" 
                  id="birthdate"
                  name="birthdate"
                  value={formData.birthdate}
                  onChange={handleChange}
                  required={!isAuthenticated}
                  max={new Date().toISOString().split('T')[0]}
                  className="form-input"
                />
              </div>
            </>
          )}

          <h3 className="text-xl font-semibold text-gray-700 border-b pb-2">Appointment Details</h3>
          
          <div>
            <label htmlFor="doctorId" className="block text-sm font-medium text-gray-700 mb-2">
              Doctor *
            </label>
            <select
              id="doctorId"
              name="doctorId"
              value={formData.doctorId}
              onChange={handleChange}
              required
              className="form-input"
            >
              <option value="">Select a doctor</option>
              {doctors.map(doctor => (
                <option key={doctor.doctorId} value={doctor.doctorId}>
                  {doctor.name} - {doctor.speciality.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label htmlFor="clinicId" className="block text-sm font-medium text-gray-700 mb-2">
              Clinic *
            </label>
            <select
              id="clinicId"
              name="clinicId"
              value={formData.clinicId}
              onChange={handleChange}
              required
              disabled={!formData.doctorId}
              className="form-input"
            >
              <option value="">Select a clinic</option>
              {availableClinics.map(clinic => (
                <option key={clinic.clinicId} value={clinic.clinicId}>
                  {clinic.name} - {clinic.address}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label htmlFor="categoryId" className="block text-sm font-medium text-gray-700 mb-2">
              Appointment Type *
            </label>
            <select
              id="categoryId"
              name="categoryId"
              value={formData.categoryId}
              onChange={handleChange}
              required
              className="form-input"
            >
              <option value="">Select appointment type</option>
              {categories.map(category => (
                <option key={category.categoryId} value={category.categoryId}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label htmlFor="appointmentDate" className="block text-sm font-medium text-gray-700 mb-2">
                Date *
              </label>
              <input
                type="date"
                id="appointmentDate"
                name="appointmentDate"
                value={formData.appointmentDate}
                onChange={handleChange}
                min={new Date().toISOString().split('T')[0]}
                required
                className="form-input"
              />
            </div>
            <div>
              <label htmlFor="appointmentTime" className="block text-sm font-medium text-gray-700 mb-2">
                Time *
              </label>
              <input
                type="time"
                id="appointmentTime"
                name="appointmentTime"
                value={formData.appointmentTime}
                onChange={handleChange}
                required
                className="form-input"
              />
            </div>
            <div>
              <label htmlFor="durationMinutes" className="block text-sm font-medium text-gray-700 mb-2">
                Duration (minutes) *
              </label>
              <select 
                name="durationMinutes" 
                id="durationMinutes"
                value={formData.durationMinutes}
                onChange={handleChange}
                required
                className="form-input"
              >
                <option value="">Select duration</option>
                <option value="15">15 minutes</option>
                <option value="30">30 minutes</option>
                <option value="45">45 minutes</option>
                <option value="60">1 hour</option>
                <option value="90">1.5 hours</option>
                <option value="120">2 hours</option>
              </select>
            </div>
          </div>

          <div>
            <label htmlFor="notes" className="block text-sm font-medium text-gray-700 mb-2">
              Additional Notes
            </label>
            <textarea
              id="notes"
              name="notes"
              value={formData.notes}
              onChange={handleChange}
              rows={4}
              placeholder="Any special requests or information..."
              className="form-input resize-none"
            />
          </div>

          <button type="submit" className="btn-primary" disabled={loading}>
            {loading ? 'Booking...' : 'Book Appointment'}
          </button>
        </form>
      </div>
    </div>
  );
};