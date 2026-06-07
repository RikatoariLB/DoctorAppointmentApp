
export const Footer = () => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="bg-gray-800 text-white text-center py-6 mt-auto">
      <div className="container mx-auto px-4">
        <p>&copy; {currentYear} Medical Appointment System. All rights reserved.</p>
      </div>
    </footer>
  );
};