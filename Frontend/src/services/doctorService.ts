
import api from './api';
import { Doctor, DoctorSearchResult, Clinic, Category, Speciality } from '@/types';

export const doctorService = {
  async getAll(): Promise<Doctor[]> {
    const response = await api.get<Doctor[]>('/doctors');
    return response.data;
  },

  async search(name: string): Promise<DoctorSearchResult[]> {
    const response = await api.get<DoctorSearchResult[]>('/doctors/search', {
      params: { name }
    });
    return response.data;
  },

  async getClinics(): Promise<Clinic[]> {
    const response = await api.get<Clinic[]>('/clinics');
    return response.data;
  },

  async getCategories(): Promise<Category[]> {
    const response = await api.get<Category[]>('/categories');
    return response.data;
  },

  async getSpecialities(): Promise<Speciality[]> {
    const response = await api.get<Speciality[]>('/specialities');
    return response.data;
  }
};