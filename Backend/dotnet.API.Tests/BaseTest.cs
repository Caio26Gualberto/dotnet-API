using Castle.MicroKernel.Registration;
using Castle.Windsor;
using dotnet_API.Interfaces;
using dotnet_API.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public abstract class BaseTest : IDisposable
{
    protected IServiceProvider _serviceProvider;
    protected BaseTest()
    {
        ConfigureServices();
    }

    private void ConfigureServices()
    {
        _serviceProvider = new ServiceCollection()
            .AddScoped<IUserService, UserService>()
            .BuildServiceProvider();
    }

    protected T GetService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }
    public void Dispose()
    {
        (_serviceProvider as IDisposable)?.Dispose();
    }
}
