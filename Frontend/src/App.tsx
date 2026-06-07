
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Layout } from './components/layout/Layout';
import { Home } from './pages/Home';
import { BookAppointment } from './pages/BookAppointment';
import { SearchDoctor } from './pages/SearchDoctor';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { MyAppointments } from './pages/MyAppointments';
import { authService } from './services/authService';

const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
  const isAuthenticated = authService.isAuthenticated();
  return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
};

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<BookAppointment />} />
          <Route path="/book" element={<BookAppointment />}/>
          <Route path="/home" element={<Home />} />
          <Route path="/search" element={<SearchDoctor />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route
            path="/my-appointments"
            element={
              <ProtectedRoute>
                <MyAppointments />
              </ProtectedRoute>
            }
          />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App
