using ProjectLottery.V1.Helpers.Utils;
using FluentMigrator;
using MySQL.Migrations.Database.DTOs;
using MySQL.Migrations.Database.DTOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesClass = ProjectLottery.V1.Entities;
using ProjectLottery.V1.Entities.Security;

namespace MySQL.Migrations.Database.Management
{
    [Migration(202409012330, "First")]
    public class _202409012330_First : Migration
    {
        private Guid AdminUserGuid = Guid.Parse("00000000-0000-1234-5678-000000000001");

        public override void Down()
        {
            Delete.Table(Tables.UserDetail).InSchema(Tables.schema);
            Delete.Table(Tables.MenuPermissions).InSchema(Tables.schema);
            Delete.Table(Tables.User).InSchema(Tables.schema);
            Delete.Table(Tables.SystemMenu).InSchema(Tables.schema);
            Delete.Table(Tables.Profiles).InSchema(Tables.schema);
            Delete.Table(Tables.City).InSchema(Tables.schema);
            Delete.Table(Tables.State).InSchema(Tables.schema);
            Delete.Table(Tables.CountryTimezone).InSchema(Tables.schema);
            Delete.Table(Tables.CountryTranslations).InSchema(Tables.schema);
            Delete.Table(Tables.Country).InSchema(Tables.schema);
            Delete.Table(Tables.Language).InSchema(Tables.schema);
            Delete.Table(Tables.Client).InSchema(Tables.schema);
        }

        public override void Up()
        {
            //Execute.Sql($"CREATE EXTENSION IF NOT EXISTS citext WITH SCHEMA {Tables.schemaPublic};");
            //Execute.Sql($"CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\" WITH SCHEMA {Tables.schemaPublic};");
            Lenguage();
            ProfilesUP();
            Countries();
            States();
            Cities();
            Users();
            UserDetails();
            Menu();
            Client();
        }

        private void ProfilesUP()
        {
            var table = Create.Table(Tables.Profiles);
            table.WithDescription("Table for storing profiles");
            table.InSchema(Tables.schema);

            FluentExtensions.CreatePK(table, Tables.Profiles);

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("user type Name");

            table
            .WithColumn(Column.Profile.Abbreviation)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Profile abbreviation");

            FluentExtensions.CreateDefaultColumns(table);

            var json = UtilsCommon.LoadJson(@"../FirstLoad/usertypes.json");
            var ProfileList = JsonConvert.DeserializeObject<List<EntitiesClass.Security.Profile>>(json);

            foreach (EntitiesClass.Security.Profile userType in ProfileList)
            {
                var row = new
                {
                    id = userType.Id,
                    name = userType.Name,
                    abbreviation = userType.Abbreviation,
                    create_by_user = AdminUserGuid.ToString()
                };

                Insert.IntoTable(Tables.Profiles).InSchema(Tables.schema).Row(row);
            }
        }

        private void Lenguage()
        {
            var table = Create.Table(Tables.Language);
            table.WithDescription("Table for storing Lenguage");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.Language);

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Lenguage Name");

            table
            .WithColumn(Column.Lenguage.Abbreviation)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("language abbreviation");

            FluentExtensions.CreateDefaultColumnsIsActive(table);
            FluentExtensions.CreateDefaultColumnsIsDeleted(table);
            FluentExtensions.CreateDefaultColumnsCreationDate(table);

            var jsonLanguages = UtilsCommon.LoadJson(@"../FirstLoad/languages.json");
            var languageList = JsonConvert.DeserializeObject<List<EntitiesClass.Global.Language>>(jsonLanguages);
            foreach (EntitiesClass.Global.Language language in languageList)
            {
                var row = new
                {
                    id = language.Id,
                    name = language.Name,
                    abbreviation = language.Abbreviation
                };

                Insert.IntoTable(Tables.Language).InSchema(Tables.schema).Row(row);
            }
        }

        private void Countries()
        {
            #region CountryTranslations

            // CountryTranslations
            var tableTras = Create.Table(Tables.CountryTranslations);
            tableTras.WithDescription("Table for Country Translations");
            tableTras.InSchema(Tables.schema);
            FluentExtensions.CreatePK(tableTras, Tables.CountryTranslations);

            tableTras
            .WithColumn(Column.CountryTranslations.CountryCode)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("Country_ID relationship field with the Country table");

            tableTras
            .WithColumn(Column.CountryTranslations.pt)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("translation of the name of the country into Portuguese.");

            tableTras
            .WithColumn(Column.CountryTranslations.es)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("translation of the name of the country into Spanish.");

            FluentExtensions.CreateDefaultColumnsIsActive(tableTras);
            FluentExtensions.CreateDefaultColumnsIsDeleted(tableTras);
            FluentExtensions.CreateDefaultColumnsCreationDate(tableTras);

            #endregion CountryTranslations

            #region CountryTimezone

            // CountryTimezone
            var tableTZ = Create.Table(Tables.CountryTimezone);
            tableTZ.WithDescription("Table for Country Timezone");
            tableTZ.InSchema(Tables.schema);
            FluentExtensions.CreatePK(tableTZ, Tables.CountryTimezone);

            tableTZ
            .WithColumn(Column.CountryTimezone.CountryCode)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("Country_ID relationship field with the Country table");

            tableTZ
            .WithColumn(Column.CountryTimezone.Abbreviation)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Abbreviation Timezone.");

            tableTZ
            .WithColumn(Column.CountryTimezone.ZoneName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Name Timezone");

            tableTZ
            .WithColumn(Column.CountryTimezone.GmtOffset)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("Statement specifies the Greenwich Mean Time offset.");

            tableTZ
            .WithColumn(Column.CountryTimezone.GmtOffsetName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("This time zone is the basis of UTC and all other time zones are based on it.");

            tableTZ
            .WithColumn(Column.CountryTimezone.TzName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("This method is used to return the time zone name of the DateTime object passed, as a string.");

            FluentExtensions.CreateDefaultColumnsIsActive(tableTZ);
            FluentExtensions.CreateDefaultColumnsIsDeleted(tableTZ);
            FluentExtensions.CreateDefaultColumnsCreationDate(tableTZ);

            #endregion CountryTimezone

            #region Country

            // Country
            var table = Create.Table(Tables.Country);
            table.WithDescription("Table for Countries");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.Country);

            table
            .WithColumn(Column.Country.CountryCode)
            .AsInt64()
            .Unique()
            .NotNullable()
            .WithColumnDescription("Country_Code relationship field with the Country table");

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Name of the country in English.");

            table
            .WithColumn(Column.Country.Abbreviation)
            .AsString(25)
            .Nullable()
            .WithColumnDescription("Country abbreviation");

            table
            .WithColumn(Column.Country.NumericCode)
            .AsString(10)
            .NotNullable()
            .WithColumnDescription("Numerical country code.");

            table
            .WithColumn(Column.Country.PhoneCode)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Country telephone code.");

            table
            .WithColumn(Column.Country.Capital)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Capital of the country.");

            table
            .WithColumn(Column.Country.Currency)
            .AsString(10)
            .NotNullable()
            .WithColumnDescription("Country currency code.");

            table
            .WithColumn(Column.Country.CurrencyName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Name of the country's currency.");

            table
            .WithColumn(Column.Country.CurrencySymbol)
            .AsString(10)
            .NotNullable()
            .WithColumnDescription("Symbol of the country's currency.");

            table
            .WithColumn(Column.Country.Region)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Region of the country..");

            table
            .WithColumn(Column.Country.SubRegion)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Subregion of the country.");

            table
            .WithColumn(Column.Country.Latitude)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Is a coordinate that specifies the north–south position of a point on the surface of the Earth or another celestial body.");

            table
            .WithColumn(Column.Country.Longitude)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Is a way to say where a place is on the Earth. It is measured starting from an imaginary north–south line called the Prime Meridian.");

            table
            .WithColumn(Column.Country.Emoji)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Emoji.");

            table
            .WithColumn(Column.Country.EmojiU)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("EmojiU.");

            FluentExtensions.CreateDefaultColumnsIsActive(table);
            FluentExtensions.CreateDefaultColumnsIsDeleted(table);
            FluentExtensions.CreateDefaultColumnsCreationDate(table);

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.CountryTranslations).InSchema(Tables.schema).ForeignColumn(Column.CountryTranslations.CountryCode)
            .ToTable(Tables.Country).InSchema(Tables.schema).PrimaryColumn(Column.Country.CountryCode);

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.CountryTimezone).InSchema(Tables.schema).ForeignColumn(Column.CountryTimezone.CountryCode)
            .ToTable(Tables.Country).InSchema(Tables.schema).PrimaryColumn(Column.Country.CountryCode);

            #endregion Country

            #region Populate Data

            var json = UtilsCommon.LoadJson(@"../FirstLoad/countries.json");
            var countriesList = JsonConvert.DeserializeObject<List<CountriesDTO>>(json).OrderBy(o => o.id);

            foreach (CountriesDTO item in countriesList)
            {
                var country_code = item.id;

                var rowCountry = new
                {
                    id = Guid.NewGuid(),
                    country_code,
                    item.name,
                    abbreviation = item.iso2,
                    item.numeric_code,
                    item.phone_code,
                    item.capital,
                    item.currency,
                    item.currency_name,
                    item.currency_symbol,
                    item.region,
                    item.subregion,
                    item.latitude,
                    item.longitude,
                    item.emoji,
                    item.emojiU
                };

                Insert.IntoTable(Tables.Country).InSchema(Tables.schema).Row(rowCountry);

                var translations = item.translations;

                var rowTrans = new
                {
                    id = Guid.NewGuid(),
                    country_code,
                    translations.pt,
                    translations.es
                };

                Insert.IntoTable(Tables.CountryTranslations).InSchema(Tables.schema).Row(rowTrans);

                var timeZoneList = item.timezones;

                foreach (var timeZone in timeZoneList)
                {
                    var rowTZ = new
                    {
                        id = Guid.NewGuid(),
                        country_code,
                        zone_name = timeZone.zoneName,
                        timeZone.gmtOffset,
                        timeZone.gmtOffsetName,
                        timeZone.abbreviation,
                        tzname = timeZone.tzName
                    };

                    Insert.IntoTable(Tables.CountryTimezone).InSchema(Tables.schema).Row(rowTZ);
                }
            }

            #endregion Populate Data
        }

        private void States()
        {
            var table = Create.Table(Tables.State);
            table.WithDescription("Table for storing States");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.State);

            table
            .WithColumn(Column.State.StateCode)
            .AsInt64()
            .NotNullable()
            .Unique()
            .WithColumnDescription("State Code.");

            table
            .WithColumn(Column.State.CountryCode)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("Country_Code relationship field with the Country table");

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Name of the state in English.");

            table
            .WithColumn(Column.State.Abbreviation)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Abreviation name of the state in English.");

            table
            .WithColumn(Column.State.Latitude)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("Is a coordinate that specifies the north–south position of a point on the surface of the Earth or another celestial body.");

            table
            .WithColumn(Column.State.Longitude)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("Is a way to say where a place is on the Earth. It is measured starting from an imaginary north–south line called the Prime Meridian.");

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.State).InSchema(Tables.schema).ForeignColumn(Column.State.CountryCode)
            .ToTable(Tables.Country).InSchema(Tables.schema).PrimaryColumn(Column.Country.CountryCode);

            var json = UtilsCommon.LoadJson(@"../FirstLoad/states.json");
            var statesList = JsonConvert.DeserializeObject<List<StatesDTO>>(json).OrderBy(o => o.id);

            foreach (StatesDTO item in statesList)
            {
                var rowState = new
                {
                    id = Guid.NewGuid(),
                    state_code = item.id,
                    country_code = item.country_id,
                    item.name,
                    abbreviation = item.state_code,
                    item.latitude,
                    item.longitude,
                };

                Insert.IntoTable(Tables.State).InSchema(Tables.schema).Row(rowState);
            }
        }

        private void Cities()
        {
            var table = Create.Table(Tables.City);
            table.WithDescription("Table for storing Cities");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.City);

            table
            .WithColumn(Column.City.CityCode)
            .AsInt64()
            .NotNullable()
            .Unique()
            .WithColumnDescription("City Code.");

            table
            .WithColumn(Column.City.StateCode)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("State_Code relationship field with the State table");

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Name of the city in English.");

            table
            .WithColumn(Column.City.Latitude)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("Is a coordinate that specifies the north–south position of a point on the surface of the Earth or another celestial body.");

            table
            .WithColumn(Column.City.Longitude)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("Is a way to say where a place is on the Earth. It is measured starting from an imaginary north–south line called the Prime Meridian.");

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.City).InSchema(Tables.schema).ForeignColumn(Column.City.StateCode)
            .ToTable(Tables.State).InSchema(Tables.schema).PrimaryColumn(Column.State.StateCode);

            var json = UtilsCommon.LoadJson(@"../FirstLoad/cities.json");
            var citiesList = JsonConvert.DeserializeObject<List<CitiesDTO>>(json).OrderBy(o => o.id);

            foreach (CitiesDTO item in citiesList)
            {
                var rowCity = new
                {
                    id = Guid.NewGuid(),
                    state_code = item.state_id,
                    city_code = item.id,
                    item.name,
                    item.latitude,
                    item.longitude,
                };

                Insert.IntoTable(Tables.City).InSchema(Tables.schema).Row(rowCity);
            }
        }

        private void Users()
        {
            var table = Create.Table(Tables.User);
            table.WithDescription("Table for storing User");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.User);

            table
            .WithColumn(Column.Users.UserName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("user Name");

            table
            .WithColumn(Column.Users.Password)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Password");

            table
            .WithColumn(Column.Users.ProfileId)
            .AsString(36)
            .NotNullable()
            .WithColumnDescription("user type ID");

            table
            .WithColumn(Column.Users.FirstName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("First Name");

            table
            .WithColumn(Column.Users.LastName)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Last Name");

            table
            .WithColumn(Column.Users.Email)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Email");

            table.WithColumn(Column.Users.IsFirstLogin)
            .AsBoolean()
            .WithDefaultValue(true)
            .WithColumnDescription("this first login from user?");

            table.WithColumn(Column.Users.ExternalCode)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("this external code from user?");

            FluentExtensions.CreateDefaultColumns(table);

            Create.ForeignKey(Tables.ForeignKey(Tables.User, Column.FormatFK("profile", Column.Id)))
            .FromTable(Tables.User).InSchema(Tables.schema)
            .ForeignColumn(Column.FormatFK("profile", Column.Id))
            .ToTable(Tables.Profiles).InSchema(Tables.schema)
            .PrimaryColumn(Column.Id);

            var json = UtilsCommon.LoadJson(@"../FirstLoad/users.json");
            var Usersist = JsonConvert.DeserializeObject<List<EntitiesClass.Security.User>>(json);

            foreach (EntitiesClass.Security.User user in Usersist)
            {
                var row = new
                {
                    id = user.Id,
                    user_name = user.UserName,
                    password = user.Password,
                    profile_id = user.ProfileId,
                    first_name = user.FirstName,
                    last_name = user.LastName,
                    email = user.Email,
                    create_by_user = user.CreateByUser,
                };

                Insert.IntoTable(Tables.User).InSchema(Tables.schema).Row(row);
            }
        }

        private void UserDetails()
        {
            var table = Create.Table(Tables.UserDetail);
            table.WithDescription("Table for storing User Details");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.UserDetail);

            table
            .WithColumn(Column.UserDetail.UserId)
            .AsString(36)
            .NotNullable()
            .WithColumnDescription("user Name");

            table
            .WithColumn(Column.UserDetail.LanguageId)
            .AsString(36)
            .NotNullable()
            .WithColumnDescription("LanguageId");

            table
            .WithColumn(Column.UserDetail.CityId)
            .AsString(36)
            .Nullable()
            .WithColumnDescription("City_id relationship field with the City table.");

            table
            .WithColumn(Column.UserDetail.Address)
            .AsString(100)
            .Nullable()
            .WithColumnDescription("User address");

            table
            .WithColumn(Column.UserDetail.Telefone)
            .AsString(40)
            .Nullable()
            .WithColumnDescription("Telephone contact of the user");

            FluentExtensions.CreateDefaultColumns(table);

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.UserDetail).InSchema(Tables.schema).ForeignColumn(Column.UserDetail.CityId)
            .ToTable(Tables.City).InSchema(Tables.schema).PrimaryColumn(Column.Id);

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.UserDetail).InSchema(Tables.schema).ForeignColumn(Column.UserDetail.LanguageId)
            .ToTable(Tables.Language).InSchema(Tables.schema).PrimaryColumn(Column.Id);

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            .FromTable(Tables.UserDetail).InSchema(Tables.schema).ForeignColumn(Column.UserDetail.UserId)
            .ToTable(Tables.User).InSchema(Tables.schema).PrimaryColumn(Column.Id);

            var json = UtilsCommon.LoadJson(@"../FirstLoad/userdetails.json");
            var UserDetailList = JsonConvert.DeserializeObject<List<EntitiesClass.Security.UserDetails>>(json);

            foreach (EntitiesClass.Security.UserDetails UserDetail in UserDetailList)
            {
                var row = new
                {
                    id = Guid.NewGuid(),
                    user_id = UserDetail.UserId,
                    language_id = UserDetail.LanguageId,
                    address = UserDetail.Address,
                    telefone = UserDetail.Telefone,
                    create_by_user = UserDetail.CreateByUser,
                };

                Insert.IntoTable(Tables.UserDetail).InSchema(Tables.schema).Row(row);
            }
        }

        private void Menu()
        {
            #region Menu

            var table = Create.Table(Tables.SystemMenu);
            table.WithDescription("Table for storing System Menu");
            table.InSchema(Tables.schema);
            FluentExtensions.CreatePK(table, Tables.SystemMenu);

            table
            .WithColumn(Column.Name)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Menu name");

            table
            .WithColumn(Column.SystemMenu.Title)
            .AsString(100)
            .NotNullable()
            .WithColumnDescription("Menu title");

            table
           .WithColumn(Column.SystemMenu.Url)
           .AsString(200)
           .NotNullable()
           .WithColumnDescription("url");

            table
            .WithColumn(Column.SystemMenu.Icon)
            .AsString(25)
            .NotNullable()
            .WithColumnDescription("Menu Icon");

            table
           .WithColumn(Column.SystemMenu.Group)
           .AsBoolean()
           .WithDefaultValue(false)
           .WithColumnDescription("Agrupador");

            table
            .WithColumn(Column.SystemMenu.ParentMenuID)
            .AsString(36)
            .Nullable()
            .WithColumnDescription("Parent menu id (null if root menu)");

            table
            .WithColumn(Column.SystemMenu.Order)
            .AsInt64()
            .NotNullable()
            .WithColumnDescription("menu display order.");

            FluentExtensions.CreateDefaultColumns(table);

            #endregion Menu

            #region Access

            var tableAcc = Create.Table(Tables.MenuPermissions);
            tableAcc.WithDescription("Table for storing System Access");
            tableAcc.InSchema(Tables.schema);
            FluentExtensions.CreatePK(tableAcc, Tables.MenuPermissions);

            tableAcc
            .WithColumn(Column.MenuPermissions.MenuId)
            .AsString(36)
            .NotNullable()
            .WithColumnDescription("Menu name");

            tableAcc
            .WithColumn(Column.MenuPermissions.ProfileId)
            .AsString(36)
            .Nullable()
            .WithColumnDescription("SubMenu name");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanView)
            .AsBoolean()
            .WithDefaultValue(true)
            .NotNullable()
            .WithColumnDescription("Can see");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanAdd)
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumnDescription("Can Add");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanEdit)
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumnDescription("Can Edit");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanDelete)
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumnDescription("Can Delete");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanExport)
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumnDescription("Can Export");

            tableAcc
            .WithColumn(Column.MenuPermissions.CanAuthorize)
            .AsBoolean()
            .WithDefaultValue(false)
            .NotNullable()
            .WithColumnDescription("Can Authorize");

            FluentExtensions.CreateDefaultColumns(tableAcc);

            //Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            //.FromTable(Tables.MenuPermissions).InSchema(Tables.schema).ForeignColumn(Column.MenuPermissions.MenuId)
            //.ToTable(Tables.SystemMenu).InSchema(Tables.schema).PrimaryColumn(Column.Id);

            //Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
            //.FromTable(Tables.MenuPermissions).InSchema(Tables.schema).ForeignColumn(Column.MenuPermissions.ProfileId)
            //.ToTable(Tables.Profiles).InSchema(Tables.schema).PrimaryColumn(Column.Id);

            #endregion Access
        }

        private void Client()
        {
            var table = Create.Table(Tables.Client);
            table.WithDescription("Table for Client");

            FluentExtensions.CreatePK(table, Tables.Client);

            table
                .WithColumn(Column.Client.CompanyName)
                    .AsString(150)
                    .NotNullable()
                    .WithColumnDescription("");

            table
              .WithColumn(Column.Client.RegisterId)
                  .AsString(30)
                  .NotNullable()
                  .WithColumnDescription("");

            table
                .WithColumn(Column.Client.FirstName)
                    .AsString(75)
                    .NotNullable()
                    .WithColumnDescription("");

            table
                .WithColumn(Column.Client.LastName)
                    .AsString(75)
                    .NotNullable()
                    .WithColumnDescription("");

            table
                .WithColumn(Column.Client.Email)
                    .AsString(150)
                    .Nullable()
                    .WithColumnDescription("");

            table
                .WithColumn(Column.Client.Phone)
                    .AsString(50)
                    .Nullable()
                    .WithColumnDescription("");

            table
                .WithColumn(Column.Client.Address)
                    .AsCustom("MEDIUMTEXT")
                    .Nullable()
                    .WithColumnDescription("");

            table
               .WithColumn(Column.Client.CityId)
                   .AsString(36)
                   .Nullable()
                   .WithColumnDescription("");

            table
               .WithColumn(Column.Client.PostalCode)
                   .AsString(36)
                   .Nullable()
                   .WithColumnDescription("");

            table
               .WithColumn(Column.Client.Status)
                   .AsString(75)
                   .NotNullable()
                   .WithColumnDescription("");

            table
                .WithColumn(Column.Client.Notes)
                    .AsCustom("MEDIUMTEXT")
                    .Nullable()
                    .WithColumnDescription("");

            FluentExtensions.CreateDefaultColumns(table);
        }
    }
}