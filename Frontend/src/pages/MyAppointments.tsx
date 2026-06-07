
import { useState, useEffect } from 'react';
import { appointmentService } from '@/services/appointmentService';
import { Appointment } from '@/types';
import { Loading } from '@/components/common/Loading';

export const MyAppointments = () => {
  const [appointments, setAppointments] = useState<Appointment[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadAppointments();
  }, []);

  const loadAppointments = async () => {
    try {
      const data = await appointmentService.getMyAppointments();
      setAppointments(data);
    } catch (err: any) {
      setError(err.response?.data || 'Failed to load appointments');
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = async (id: number) => {
    if (!confirm('Are you sure you want to cancel this appointment?')) return;

    try {
      await appointmentService.cancel(id);
      loadAppointments();
    } catch (err: any) {
      setError(err.response?.data || 'Failed to cancel appointment');
    }
  };

  if (loading) return <Loading />;

  return (
    <div className="max-w-4xl mx-auto">
      <div className="bg-white rounded-lg shadow-lg p-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-2">My Appointments</h1>
        <p className="text-gray-600 mb-6">View and manage your scheduled appointments</p>

        {error && (
          <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 mb-6 rounded">
            {error}
          </div>
        )}

        {appointments.length === 0 ? (
          <div className="text-center py-12">
            <p className="text-gray-600 mb-6 text-lg">You don't have any appointments yet.</p>
            <a 
              href="/book" 
              className="inline-block bg-blue-500 hover:bg-blue-600 text-white font-semibold py-3 px-8 rounded-lg transition-colors"
            >
              Book an Appointment
            </a>
          </div>
        ) : (
          <div className="space-y-4">
            {appointments.map(apt => (
              <div key={apt.appointmentId} className="bg-gray-50 border border-gray-200 rounded-lg p-6">
                <div className="flex justify-between items-start mb-4 pb-4 border-b border-gray-200">
                  <h3 className="text-xl font-bold text-gray-800">{apt.doctor?.name}</h3>
                  <span className={`px-3 py-1 rounded-full text-sm font-semibold ${
                    apt.status === 'Scheduled' 
                      ? 'bg-green-100 text-green-800' 
                      : 'bg-red-100 text-red-800'
                  }`}>
                    {apt.status}
                  </span>
                </div>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-3 text-gray-700 mb-4">
                  <p><span className="font-semibold">Date:</span> {new Date(apt.appointmentDate).toLocaleDateString()}</p>
                  <p><span className="font-semibold">Time:</span> {apt.timeSlot}</p>
                  <p><span className="font-semibold">Clinic:</span> {apt.clinic?.name}</p>
                  <p><span className="font-semibold">Type:</span> {apt.category?.name}</p>
                </div>
                {apt.notes && (
                  <p className="text-gray-600 mb-4">
                    <span className="font-semibold">Notes:</span> {apt.notes}
                  </p>
                )}
                {apt.status !== 'Cancelled' && (
                  <button
                    onClick={() => handleCancel(apt.appointmentId)}
                    className="btn-danger w-full md:w-auto"
                  >
                    Cancel Appointment
                  </button>
                )}
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};