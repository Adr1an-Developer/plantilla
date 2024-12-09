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
    public class CountryRepository : GenericReadOnlyRepository<Country>, ICountryRepository
    {
        public CountryRepository(IEfDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Country>> GetAllCountrys()
        {
            var rows = await (from c in Context.GetDbSet<Country>()
                              where c.IsActive == true && c.IsDeleted == false
                              select new Country
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  Abbreviation = c.Abbreviation,
                                  Region = c.Region,
                                  SubRegion = c.SubRegion,
                                  Capital = c.Capital,
                                  PhoneCode = c.PhoneCode,
                                  CountryCode = c.CountryCode,
                                  NumericCode = c.NumericCode,
                                  Currency = c.Currency,
                                  CurrencyName = c.CurrencyName,
                                  CurrencySymbol = c.CurrencySymbol,
                                  Latitude = c.Latitude,
                                  Longitude = c.Longitude,
                                  Emoji = c.Emoji,
                                  EmojiU = c.EmojiU,
                                  IsActive = c.IsActive,
                                  IsDeleted = c.IsDeleted,
                                  CreationDate = c.CreationDate
                              }).ToListAsync();
            return rows;
        }

        public async Task<Country> GetCountryByID(string id)
        {
            var row = await (from c in Context.GetDbSet<Country>()
                             where c.IsActive == true && c.IsDeleted == false
                             && c.Id == id
                             select new Country
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Abbreviation = c.Abbreviation,
                                 Region = c.Region,
                                 SubRegion = c.SubRegion,
                                 Capital = c.Capital,
                                 PhoneCode = c.PhoneCode,
                                 CountryCode = c.CountryCode,
                                 NumericCode = c.NumericCode,
                                 Currency = c.Currency,
                                 CurrencyName = c.CurrencyName,
                                 CurrencySymbol = c.CurrencySymbol,
                                 Latitude = c.Latitude,
                                 Longitude = c.Longitude,
                                 Emoji = c.Emoji,
                                 EmojiU = c.EmojiU,
                                 IsActive = c.IsActive,
                                 IsDeleted = c.IsDeleted,
                                 CreationDate = c.CreationDate
                             }).FirstOrDefaultAsync();
            return row;
        }

        public async Task<Country> GetCountryByName(string CountryName)
        {
            var row = await (from c in Context.GetDbSet<Country>()
                             where c.IsActive == true && c.IsDeleted == false
                             && c.Name.ToLower() == CountryName.ToLower().Trim()
                             select new Country
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Abbreviation = c.Abbreviation,
                                 Region = c.Region,
                                 SubRegion = c.SubRegion,
                                 Capital = c.Capital,
                                 PhoneCode = c.PhoneCode,
                                 CountryCode = c.CountryCode,
                                 NumericCode = c.NumericCode,
                                 Currency = c.Currency,
                                 CurrencyName = c.CurrencyName,
                                 CurrencySymbol = c.CurrencySymbol,
                                 Latitude = c.Latitude,
                                 Longitude = c.Longitude,
                                 Emoji = c.Emoji,
                                 EmojiU = c.EmojiU,
                                 IsActive = c.IsActive,
                                 IsDeleted = c.IsDeleted,
                                 CreationDate = c.CreationDate
                             }).FirstOrDefaultAsync();
            return row;
        }
    }
}