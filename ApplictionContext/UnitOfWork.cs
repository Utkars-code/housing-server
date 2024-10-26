using webApi_build_Real.Models;
using webApi_build_Real.Repository.Adstract;
using webApi_build_Real.Repository.implementation;

namespace webApi_build_Real.ApplictionContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dc;
        public UnitOfWork(DataContext dc)
        {
            _dc = dc;
        }

        public ICityRepository CityRepository => 
             new CityRepository(_dc);

   public IUserRepository UserRepository=>
            new UserRepository(_dc);
        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}
