using Application.Services.Users;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
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

        //container.Register(c =>
        //{
        //    var configuration = c.Resolve<IConfiguration>();
        //    var optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    return new EFDataContext(optionsBuilder.Options);
        //}).As<EFDataContext>().SingleInstance();


        container.RegisterAssemblyTypes(
           applicationAssembly,
           commonAssembly,
           presentationAssembly)
        .Where(t => typeof(IScope).IsAssignableFrom(t))  // اضافه کردن شرط برای ارث‌بری از IScope
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(infrastructureAssembly)
         .AssignableTo<IRepository>()
         .AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        container.RegisterAssemblyTypes(applicationAssembly)
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
