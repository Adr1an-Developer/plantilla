using Autofac;
using ProjectLottery.V1.Domain.Services.Abstractions;
using ProjectLottery.V1.Domain.Services.Abstractions.Config;
using ProjectLottery.V1.Domain.Services.Abstractions.Regions;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;
using ProjectLottery.V1.Domain.Services.Abstractions.System;
using ProjectLottery.V1.Domain.Services.Implementations;
using ProjectLottery.V1.Domain.Services.Implementations.Config;
using ProjectLottery.V1.Domain.Services.Implementations.Regions;
using ProjectLottery.V1.Domain.Services.Implementations.Security;
using ProjectLottery.V1.Domain.Services.Implementations.System;

namespace ProjectLottery.V1.AutofacModule
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ServiceBase<>))
            .As(typeof(IServiceBase<>));

            builder.RegisterGeneric(typeof(ServiceReadOnlyBase<>))
            .As(typeof(IServiceReadOnlyBase<>));

            builder
            .RegisterType<AuthorizeServices>()
            .As<IAuthorizeServices>()
            .InstancePerLifetimeScope(); ;

            builder
            .RegisterType<UserServices>()
            .As<IUserServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<ProfileServices>()
            .As<IProfileServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<SystemMenuServices>()
            .As<ISystemMenuServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<MenuPermissionServices>()
            .As<IMenuPermissionServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<CountryServices>()
            .As<ICountryServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<StateServices>()
            .As<IStateServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<CityServices>()
            .As<ICityServices>()
            .InstancePerLifetimeScope();

            builder
            .RegisterType<ClientServices>()
            .As<IClientServices>()
            .InstancePerLifetimeScope();
        }
    }
}