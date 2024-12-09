using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.System;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Domain.Data.Repository.System
{
    public class ClientRepository : GenericRepository<SystemClient>, IClientRepository
    {
        public ClientRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SystemClient>> GetAllSystemClient()
        {
            var rows = await (from c in Context.GetDbSet<SystemClient>()
                              join ljCity in Context.GetDbSet<City>() on c.CityId equals ljCity.Id into city
                              from ct in city.DefaultIfEmpty()
                              join ljState in Context.GetDbSet<State>() on ct.StateCode equals ljState.StateCode into state
                              from st in state.DefaultIfEmpty()
                              join ljCountry in Context.GetDbSet<Country>() on st.CountryCode equals ljCountry.CountryCode into country
                              from cty in country.DefaultIfEmpty()
                              where c.IsDeleted == false
                              select new SystemClient
                              {
                                  Id = c.Id,
                                  Register_Id = c.Register_Id,
                                  ClientTypeId = c.ClientTypeId,
                                  CompanyName = c.CompanyName,
                                  FirstName = c.FirstName,
                                  LastName = c.LastName,
                                  Email = c.Email,
                                  Phone = c.Phone,
                                  CityId = c.CityId,
                                  CityName = ct.Name,
                                  StateId = st.Id,
                                  StateName = st.Name,
                                  CountryId = cty.Id,
                                  CountryName = cty.Name,
                                  Address = c.Address,
                                  PostalCode = c.PostalCode,
                                  Status = c.Status,
                                  Notes = c.Notes,
                                  CreateByUser = c.CreateByUser,
                                  UpdateByUser = c.UpdateByUser,
                                  IsActive = c.IsActive,
                                  IsDeleted = c.IsDeleted,
                                  CreationDate = c.CreationDate,
                                  ModificationDate = c.ModificationDate,
                              }
                              ).ToListAsync();
            return rows;
        }

        public async Task<SystemClient> GetSystemClientById(string id)
        {
            var row = await (from c in Context.GetDbSet<SystemClient>()
                             join ljCity in Context.GetDbSet<City>() on c.CityId equals ljCity.Id into city
                             from ct in city.DefaultIfEmpty()
                             join ljState in Context.GetDbSet<State>() on ct.StateCode equals ljState.StateCode into state
                             from st in state.DefaultIfEmpty()
                             join ljCountry in Context.GetDbSet<Country>() on st.CountryCode equals ljCountry.CountryCode into country
                             from cty in country.DefaultIfEmpty()
                             where c.IsDeleted == false && c.Id == id
                             select new SystemClient
                             {
                                 Id = c.Id,
                                 Register_Id = c.Register_Id,
                                 ClientTypeId = c.ClientTypeId,
                                 CompanyName = c.CompanyName,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 Email = c.Email,
                                 Phone = c.Phone,
                                 CityId = c.CityId,
                                 CityName = ct.Name,
                                 StateId = st.Id,
                                 StateName = st.Name,
                                 CountryId = cty.Id,
                                 CountryName = cty.Name,
                                 Address = c.Address,
                                 PostalCode = c.PostalCode,
                                 Status = c.Status,
                                 Notes = c.Notes,
                                 CreateByUser = c.CreateByUser,
                                 UpdateByUser = c.UpdateByUser,
                                 IsActive = c.IsActive,
                                 IsDeleted = c.IsDeleted,
                                 CreationDate = c.CreationDate,
                                 ModificationDate = c.ModificationDate,
                             }
                               ).FirstOrDefaultAsync();
            return row;
        }

        public async Task<SystemClient> GetSystemClientByName(string clientName)
        {
            var row = await (from c in Context.GetDbSet<SystemClient>()
                             join ljCity in Context.GetDbSet<City>() on c.CityId equals ljCity.Id into city
                             from ct in city.DefaultIfEmpty()
                             join ljState in Context.GetDbSet<State>() on ct.StateCode equals ljState.StateCode into state
                             from st in state.DefaultIfEmpty()
                             join ljCountry in Context.GetDbSet<Country>() on st.CountryCode equals ljCountry.CountryCode into country
                             from cty in country.DefaultIfEmpty()
                             where c.IsDeleted == false && c.CompanyName.ToLower() == clientName.ToLower().Trim()
                             select new SystemClient
                             {
                                 Id = c.Id,
                                 Register_Id = c.Register_Id,
                                 ClientTypeId = c.ClientTypeId,
                                 CompanyName = c.CompanyName,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 Email = c.Email,
                                 Phone = c.Phone,
                                 CityId = c.CityId,
                                 CityName = ct.Name,
                                 StateId = st.Id,
                                 StateName = st.Name,
                                 CountryId = cty.Id,
                                 CountryName = cty.Name,
                                 Address = c.Address,
                                 PostalCode = c.PostalCode,
                                 Status = c.Status,
                                 Notes = c.Notes,
                                 CreateByUser = c.CreateByUser,
                                 UpdateByUser = c.UpdateByUser,
                                 IsActive = c.IsActive,
                                 IsDeleted = c.IsDeleted,
                                 CreationDate = c.CreationDate,
                                 ModificationDate = c.ModificationDate,
                             }
                              ).FirstOrDefaultAsync();
            return row;
        }
    }
}