
import { Link, useNavigate } from 'react-router-dom';
import { authService } from '@/services/authService';

export const Header = () => {
  const navigate = useNavigate();
  const isAuthenticated = authService.isAuthenticated();

  const handleLogout = () => {
    authService.logout();
    navigate('/login');
  };

  return (
    <header className="bg-gray-800 text-white shadow-lg">
      <div className="container mx-auto px-4">
        <div className="flex flex-wrap items-center justify-between py-4">
          <h1 className="text-2xl font-bold">
            <Link to="/home" className="hover:text-blue-300 transition-colors">
              Medical Appointments
            </Link>
          </h1>
          <nav className="flex flex-wrap items-center gap-6">
            <Link 
              to="/" 
              className="hover:text-blue-300 transition-colors"
            >
              Book Appointment
            </Link>
            <Link 
              to="/search" 
              className="hover:text-blue-300 transition-colors"
            >
              Search Doctors
            </Link>
            {isAuthenticated ? (
              <>
                <Link 
                  to="/my-appointments" 
                  className="hover:text-blue-300 transition-colors"
                >
                  My Appointments
                </Link>
                <button
                  onClick={handleLogout}
                  className="bg-red-600 hover:bg-red-700 px-4 py-2 rounded-lg transition-colors"
                >
                  Logout
                </button>
              </>
            ) : (
              <>
                <Link 
                  to="/login" 
                  className="hover:text-blue-300 transition-colors"
                >
                  Login
                </Link>
                <Link 
                  to="/register"
                  className="bg-blue-600 hover:bg-blue-700 px-4 py-2 rounded-lg transition-colors"
                >
                  Register
                </Link>
              </>
            )}
          </nav>
        </div>
      </div>
    </header>
  );
};