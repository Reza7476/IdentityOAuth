using Application.Services.Users;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using Infrastructure;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Configurations.AutoFacts;

public static class ConfigAutofac
{
    public static ConfigureHostBuilder AddAutoFact(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutoFacModule());
        });

        return builder;
    }
}

public class AutoFacModule : Module
{
    protected override void Load(ContainerBuilder container)
    {
        var applicationAssembly = typeof(UserAppService).Assembly;
        var commonAssembly = typeof(IRepository).Assembly;
        var presentationAssembly=typeof(ConfigAutofac).Assembly;
        var infrastructureAssembly=typeof(EFDataContext).Assembly;


        container.Register(c =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(InfraStructureHelper.GetInfrastructureDirectory())
                .AddJsonFile("InfraAppSetting.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }).As<IConfiguration>().SingleInstance();
        container.RegisterType<ConfigurationService>().AsSelf().InstancePerLifetimeScope();

        container.Register(c =>
        {
            var configurationService = c.Resolve<ConfigurationService>();  // دریافت ConfigurationService از کانتینر
            var connectionString = configurationService.GetConnectionString();  // دریافت کانکشن استرینگ

            var optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
            optionsBuilder.UseSqlServer(connectionString);  // اتصال به SQL Server

            return new EFDataContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();


        container.RegisterAssemblyTypes(
           applicationAssembly,
           commonAssembly,
           infrastructureAssembly,
           presentationAssembly)
        .Where(t => typeof(IScope).IsAssignableFrom(t))  // اضافه کردن شرط برای ارث‌بری از IScope
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(infrastructureAssembly)
         .AssignableTo<IRepository>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(
            applicationAssembly,
            presentationAssembly)
        .AssignableTo<IService>()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        container.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();

        base.Load(container);
    }
}
