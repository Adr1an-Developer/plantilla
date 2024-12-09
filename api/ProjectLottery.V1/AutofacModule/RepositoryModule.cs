using Autofac;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Config;
using ProjectLottery.V1.Domain.Data.Abstractions.Regions;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Domain.Data.Abstractions.System;
using ProjectLottery.V1.Domain.Data.Repository;
using ProjectLottery.V1.Domain.Data.Repository.Config;
using ProjectLottery.V1.Domain.Data.Repository.Regions;
using ProjectLottery.V1.Domain.Data.Repository.Security;
using ProjectLottery.V1.Domain.Data.Repository.System;

namespace ProjectLottery.V1.AutofacModule
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericReadOnlyRepository<>))
               .As(typeof(IGenericReadOnlyRepository<>))
               .InstancePerLifetimeScope();

            builder.RegisterType<JWTManagerRepository>().As<IJWTManagerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SystemMenuRepository>().As<ISystemMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuPermissionRepository>().As<IMenuPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StateRepository>().As<IStateRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerLifetimeScope();
        }
    }
}