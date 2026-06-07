
import { Link } from 'react-router-dom';
import { authService } from '@/services/authService';

export const Home = () => {
  const isAuthenticated = authService.isAuthenticated();

  return (
    <div className="min-h-[calc(100vh-200px)] flex items-center justify-center bg-gradient-to-br from-blue-50 to-indigo-100">
      <div className="max-w-4xl mx-auto px-4 py-12 text-center">
        <h1 className="text-5xl md:text-6xl font-bold text-gray-900 mb-6">
          Welcome to Medical Appointments
        </h1>
        <p className="text-xl text-gray-600 mb-12 max-w-2xl mx-auto">
          Book appointments with top healthcare professionals. Quick, easy, and secure.
        </p>

        <div className="grid md:grid-cols-2 gap-6 mb-12">
          <div className="bg-white rounded-xl shadow-lg p-8 hover:shadow-xl transition-shadow">
            <div className="text-4xl mb-4">📅</div>
            <h3 className="text-2xl font-semibold mb-3 text-gray-900">
              Book Appointment
            </h3>
            <p className="text-gray-600 mb-6">
              Schedule your appointment with our experienced doctors. Available for both registered users and guests.
            </p>
            <Link
              to="/book"
              className="inline-block bg-blue-600 hover:bg-blue-700 text-white font-semibold px-8 py-3 rounded-lg transition-colors"
            >
              Book Now
            </Link>
          </div>

          <div className="bg-white rounded-xl shadow-lg p-8 hover:shadow-xl transition-shadow">
            <div className="text-4xl mb-4">🔍</div>
            <h3 className="text-2xl font-semibold mb-3 text-gray-900">
              Find a Doctor
            </h3>
            <p className="text-gray-600 mb-6">
              Search for doctors by name or specialty. View their clinic information and availability.
            </p>
            <Link
              to="/search"
              className="inline-block bg-green-600 hover:bg-green-700 text-white font-semibold px-8 py-3 rounded-lg transition-colors"
            >
              Search Doctors
            </Link>
          </div>
        </div>

        {isAuthenticated ? (
          <div className="bg-white rounded-xl shadow-lg p-8">
            <div className="text-4xl mb-4">📋</div>
            <h3 className="text-2xl font-semibold mb-3 text-gray-900">
              Your Appointments
            </h3>
            <p className="text-gray-600 mb-6">
              View and manage your scheduled appointments
            </p>
            <Link
              to="/my-appointments"
              className="inline-block bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-8 py-3 rounded-lg transition-colors"
            >
              View My Appointments
            </Link>
          </div>
        ) : (
          <div className="bg-white rounded-xl shadow-lg p-8">
            <h3 className="text-2xl font-semibold mb-3 text-gray-900">
              Already have an account?
            </h3>
            <p className="text-gray-600 mb-6">
              Login to view your appointments and medical history
            </p>
            <div className="flex gap-4 justify-center">
              <Link
                to="/login"
                className="inline-block bg-gray-700 hover:bg-gray-800 text-white font-semibold px-8 py-3 rounded-lg transition-colors"
              >
                Login
              </Link>
              <Link
                to="/register"
                className="inline-block bg-blue-600 hover:bg-blue-700 text-white font-semibold px-8 py-3 rounded-lg transition-colors"
              >
                Create Account
              </Link>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};