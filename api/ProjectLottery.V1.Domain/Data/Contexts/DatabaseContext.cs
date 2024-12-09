using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.Security;
using Microsoft.EntityFrameworkCore;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Domain.Data.Contexts
{
    public class DatabaseContext : EfDbContextBase
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User>? Users
        {
            get; set;
        }

        public DbSet<UserDetails>? UserDetails
        {
            get; set;
        }

        public DbSet<Profile>? UserType
        {
            get; set;
        }

        public DbSet<Language>? Language
        {
            get; set;
        }

        public DbSet<ExceptionLog> exceptionLogs
        {
            get; set;
        }

        public DbSet<Country> Countries
        {
            get; set;
        }

        public DbSet<SystemClient> SystemClients
        {
            get; set;
        }

        public DbSet<SystemMenu> systemMenus
        {
            get; set;
        }

        public DbSet<MenuPermission> menuPermissions
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}