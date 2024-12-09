using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Regions;
using ProjectLottery.V1.Entities.Global;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Repository.Regions
{
    public class CityRepository : GenericReadOnlyRepository<City>, ICityRepository
    {
        public CityRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            var rows = await (from c in Context.GetDbSet<City>()
                              select new City
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  StateCode = c.StateCode,
                                  CityCode = c.CityCode,
                                  Latitude = c.Latitude,
                                  Longitude = c.Longitude,
                              }).ToListAsync();
            return rows;
        }

        public async Task<IEnumerable<City>> GetCitiesByStateId(string stateId)
        {
            var rows = await (from c in Context.GetDbSet<City>()
                              join s in Context.GetDbSet<State>() on c.StateCode equals s.StateCode
                              where s.Id == stateId
                              select new City
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  StateCode = c.StateCode,
                                  CityCode = c.CityCode,
                                  Latitude = c.Latitude,
                                  Longitude = c.Longitude,
                              }).ToListAsync();
            return rows;
        }

        public async Task<City> GetCityByID(string id)
        {
            var row = await (from c in Context.GetDbSet<City>()
                             where c.Id == id
                             select new City
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 CityCode = c.CityCode,
                                 StateCode = c.StateCode,
                                 Latitude = c.Latitude,
                                 Longitude = c.Longitude,
                             }).FirstOrDefaultAsync();
            return row;
        }

        public async Task<City> GetCityByName(string CityName)
        {
            var row = await (from c in Context.GetDbSet<City>()
                             where c.Name.ToLower() == CityName.ToLower().Trim()
                             select new City
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 CityCode = c.CityCode,
                                 StateCode = c.StateCode,
                                 Latitude = c.Latitude,
                                 Longitude = c.Longitude,
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}