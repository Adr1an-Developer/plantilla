using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Regions;
using ProjectLottery.V1.Entities.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Repository.Regions
{
    public class StateRepository : GenericReadOnlyRepository<State>, IStateRepository
    {
        public StateRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<State>> GetAllStates()
        {
            var rows = await (from s in Context.GetDbSet<State>()
                              select new State
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Abbreviation = s.Abbreviation,
                                  CountryCode = s.CountryCode,
                                  StateCode = s.StateCode,
                                  Latitude = s.Latitude,
                                  Longitude = s.Longitude,
                              }).ToListAsync();
            return rows;
        }

        public async Task<IEnumerable<State>> GetStatesByCountryId(string countryId)
        {
            var rows = await (from s in Context.GetDbSet<State>()
                              join c in Context.GetDbSet<Country>() on s.CountryCode equals c.CountryCode
                              where c.Id == countryId
                              select new State
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Abbreviation = s.Abbreviation,
                                  CountryCode = s.CountryCode,
                                  StateCode = s.StateCode,
                                  Latitude = s.Latitude,
                                  Longitude = s.Longitude,
                              }).ToListAsync();
            return rows;
        }

        public async Task<State> GetStateByID(string id)
        {
            var row = await (from s in Context.GetDbSet<State>()
                             where s.Id == id
                             select new State
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Abbreviation = s.Abbreviation,
                                 CountryCode = s.CountryCode,
                                 StateCode = s.StateCode,
                                 Latitude = s.Latitude,
                                 Longitude = s.Longitude,
                             }).FirstOrDefaultAsync();
            return row;
        }

        public async Task<State> GetStateByName(string stateName)
        {
            var row = await (from s in Context.GetDbSet<State>()
                             where s.Name.ToLower() == stateName.ToLower().Trim()
                             select new State
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Abbreviation = s.Abbreviation,
                                 CountryCode = s.CountryCode,
                                 StateCode = s.StateCode,
                                 Latitude = s.Latitude,
                                 Longitude = s.Longitude,
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}