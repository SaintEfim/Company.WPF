using System.Windows;
using Autofac;
using AutoMapper;
using Company.Domain;
using Company.WPF.Services;
using Company.WPF.ViewModels;
using Company.WPF.Views;
using Microsoft.Extensions.Configuration;

namespace Company.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IContainer _container = null!;

    protected override void OnStartup(
        StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = new ContainerBuilder();

        ConfigureServices(builder);
        RegisterConfiguration(builder);
        _container = builder.Build();

        var mainWindow = _container.Resolve<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices(
        ContainerBuilder builder)
    {
        builder.RegisterModule<CompanyDomainModule>();

        builder.RegisterAssemblyTypes(typeof(AutoMapperProfile).Assembly)
            .As<Profile>()
            .SingleInstance();

        builder.Register(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var profile in ctx.Resolve<IEnumerable<Profile>>())
                    {
                        cfg.AddProfile(profile);
                    }
                });
                return config.CreateMapper();
            })
            .As<IMapper>()
            .SingleInstance();

        builder.RegisterType<WindowService>()
            .As<IWindowService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<MainViewModel>()
            .AsSelf()
            .InstancePerLifetimeScope();
        builder.RegisterType<EmployeeEditViewModel>()
            .AsSelf()
            .InstancePerLifetimeScope();
        builder.RegisterType<ContractorEditViewModel>()
            .AsSelf()
            .InstancePerLifetimeScope();
        builder.RegisterType<OrderEditViewModel>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterType<MainWindow>()
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterType<OrderEditWindow>()
            .AsSelf()
            .InstancePerDependency();

        builder.RegisterType<EmployeeEditWindow>()
            .AsSelf()
            .InstancePerDependency();

        builder.RegisterType<ContractorEditWindow>()
            .AsSelf()
            .InstancePerDependency();
    }

    private static void RegisterConfiguration(
        ContainerBuilder builder)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // 2. Регистрируем экземпляр
        builder.RegisterInstance(configuration)
            .As<IConfiguration>()
            .SingleInstance();
    }
}
