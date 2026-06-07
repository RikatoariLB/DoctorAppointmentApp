
import api from './api';
import { Appointment, CreateAppointmentDto } from '@/types';

export const appointmentService = {
  async create(data: CreateAppointmentDto): Promise<Appointment> {
    const response = await api.post<Appointment>('/appointments', data);
    return response.data;
  },

  async getMyAppointments(): Promise<Appointment[]> {
    const response = await api.get<Appointment[]>('/appointments');
    return response.data;
  },

  async update(id: number, data: Partial<CreateAppointmentDto>): Promise<void> {
    await api.put(`/appointments/${id}`, data);
  },

  async cancel(id: number): Promise<void> {
    await api.delete(`/appointments/${id}`);
  }
};