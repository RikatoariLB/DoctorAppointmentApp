
import { useState, useEffect } from 'react';
import { doctorService } from '@/services/doctorService';
import { Doctor } from '@/types';
import { Loading } from '@/components/common/Loading';

export const SearchDoctor = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [allDoctors, setAllDoctors] = useState<Doctor[]>([]);
  const [filteredDoctors, setFilteredDoctors] = useState<Doctor[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadAllDoctors();
  }, []);

  const loadAllDoctors = async () => {
    setLoading(true);
    setError('');
    try {
      const data = await doctorService.getAll();
      setAllDoctors(data);
      setFilteredDoctors(data);
    } catch (err: any) {
      setError(err.response?.data || 'Failed to load doctors');
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    filterDoctors(searchTerm);
  };

  const handleSearchChange = (value: string) => {
    setSearchTerm(value);
    filterDoctors(value);
  };

  const filterDoctors = (term: string) => {
    if (!term.trim()) {
      setFilteredDoctors(allDoctors);
      return;
    }

    const filtered = allDoctors.filter(doctor =>
      doctor.name.toLowerCase().includes(term.toLowerCase())
    );
    setFilteredDoctors(filtered);
  };

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="max-w-4xl mx-auto">
      <div className="bg-white rounded-lg shadow-lg p-8">
        <h1 className="text-3xl font-bold text-gray-800 mb-2">Search Doctors</h1>
        <p className="text-gray-600 mb-6">
          {searchTerm ? 'Search results' : 'Showing all doctors'}
        </p>

        <form onSubmit={handleSearch} className="mb-8">
          <div className="flex gap-4">
            <input
              type="text"
              value={searchTerm}
              onChange={(e) => handleSearchChange(e.target.value)}
              placeholder="Search by doctor name..."
              className="form-input flex-1"
            />
            <button 
              type="submit" 
              className="bg-blue-500 hover:bg-blue-600 text-white font-semibold py-2 px-8 rounded-lg transition-colors duration-200"
            >
              Search
            </button>
          </div>
        </form>

        {error && (
          <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 rounded mb-6">
            {error}
          </div>
        )}

        {filteredDoctors.length === 0 ? (
          <div className="text-center py-8 text-gray-500">
            <p className="text-lg">No doctors found</p>
            {searchTerm && (
              <button
                onClick={() => handleSearchChange('')}
                className="mt-4 text-blue-600 hover:text-blue-700 underline"
              >
                Clear search
              </button>
            )}
          </div>
        ) : (
          <div>
            <h2 className="text-2xl font-semibold text-gray-800 mb-4">
              {searchTerm ? `Search Results (${filteredDoctors.length})` : `All Doctors (${filteredDoctors.length})`}
            </h2>
            <div className="space-y-4">
              {filteredDoctors.map((doctor) => (
                <div 
                  key={doctor.doctorId} 
                  className="bg-gray-50 border border-gray-200 rounded-lg p-6 hover:shadow-md transition-shadow"
                >
                  <h3 className="text-xl font-bold text-gray-800 mb-2">
                    {doctor.name}
                  </h3>
                  <p className="text-blue-600 font-medium mb-1">
                    {doctor.speciality.name}
                  </p>
                  <p className="text-gray-600 mb-1">
                    <span className="font-semibold">Clinic:</span> {doctor.clinic.name}
                  </p>
                  <p className="text-gray-500 text-sm">
                    📍 {doctor.clinic.address}
                  </p>
                  <p className="text-gray-500 text-sm">
                    📞 {doctor.clinic.phone}
                  </p>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};