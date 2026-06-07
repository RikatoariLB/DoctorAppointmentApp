
export interface Patient {
  patientId: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  ssn?: string;
  birthdate?: Date;
  gender?: string;
  isRegistered: boolean;
}

export interface Doctor {
  doctorId: number;
  name: string;
  email: string;
  specialityId: number;
  clinicId: number;
  speciality: Speciality;
  clinic: Clinic;
}

export interface Clinic {
  clinicId: number;
  name: string;
  address: string;
  phone: string;
}

export interface Speciality {
  specialityId: number;
  name: string;
}

export interface Category {
  categoryId: number;
  name: string;
  description?: string;
}

export interface Appointment {
  appointmentId: number;
  patientId: number;
  doctorId: number;
  clinicId: number;
  categoryId: number;
  appointmentDate: Date;
  timeSlot: string;
  status: string;
  notes?: string;
  patient?: Patient;
  doctor?: Doctor;
  clinic?: Clinic;
  category?: Category;
}

export interface CreateAppointmentDto {
  firstName?: string;
  lastName?: string;
  email?: string;
  phone?: string;
  birthdate?: Date;
  doctorId: number;
  clinicId: number;
  categoryId: number;
  appointmentDate: Date;
  timeSlot: string;
  durationMinutes: number;
  notes?: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phone: string;
  ssn?: string;
  birthdate?: Date;
  gender?: string;
}

export interface AuthResponse {
  token: string;
  patientId: number;
  email: string;
  fullName: string;
}

export interface DoctorSearchResult {
  fullName: string;
  clinicName: string;
  specialityName: string;
}