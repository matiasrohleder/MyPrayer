using System;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.BusinessLogic;
using BusinessLayer.Configurations;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Interfaces;
using DataLayer.Services;
using Entities.Models;
using Entities.Models.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        // Set up dependency injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(_ => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .Build())
            // .AddSingleton<IConfiguration>(x => new MyPrayerConfiguration(x.GetRequiredService<IWebHostEnvironment>(), x.GetRequiredService<IServiceProvider>()))
            .AddScoped<IBibleConfiguration, BibleConfiguration>()
            .AddTransient<IReadingBusinessLogic, ReadingBusinessLogic>() // Register ReadingBusinessLogic
            .AddTransient<IService<Reading>, Service<Reading>>() // Register Service<Reading>
            .AddDbContext<ModelsDbContext>()  // Register ModelsDbContext
            .AddScoped<IRepositoryFactory, RepositoryFactory>() // Register RepositoryFactory
            .AddScoped<IUnitOfWork>((provider) =>
            {
                ModelsDbContext modelDbContext = provider.GetRequiredService<ModelsDbContext>();
                IRepositoryFactory repositoryFactory = provider.GetRequiredService<IRepositoryFactory>();
                return new UnitOfWork(modelDbContext, repositoryFactory);
            })
            .AddHttpClient()
            .BuildServiceProvider();

        // Resolve the service and call the method
        var readingBusinessLogic = serviceProvider.GetRequiredService<IReadingBusinessLogic>();
        await readingBusinessLogic.GetReadings();

        Console.WriteLine("Job executed successfully.");
    }
}
