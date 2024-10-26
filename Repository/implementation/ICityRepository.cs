using webApi_build_Real.Models;

namespace webApi_build_Real.Repository.implementation
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int CityId);
     //   Task<bool> SaveAsync();
        Task<City> FindCity(int id);
    }
}
