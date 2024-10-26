using Microsoft.EntityFrameworkCore;
using webApi_build_Real.ApplictionContext;
using webApi_build_Real.Models;
using webApi_build_Real.Repository.implementation;

namespace webApi_build_Real.Repository.Adstract
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _dc;
        public CityRepository(DataContext dc)
        {
            _dc = dc;
        }
        public void AddCity(City city)
        {
            _dc.Cities.AddAsync(city);
        }

        public void DeleteCity(int CityId)
        {
            var city = _dc.Cities.Find(CityId);
                _dc.Cities.Remove(city);

        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
           return await _dc.Cities.ToListAsync();
        }
        //using for save data in data base-->
        //public async Task<bool> SaveAsync()
        //{
        //    return await _dc.SaveChangesAsync() > 0;
        //}
        public async Task<City> FindCity(int id)
        {
           return await _dc.Cities.FindAsync(id);
        }
    }
}
