using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demo;
using Demo.Application.Features.Books.Commands;
using Demo.Data;
using Demo.Infrastructure;
using Demo.Models.Demo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateBootstrapLogger();
try
{
    Log.Information("Application Staring.......");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly();

    #region Autofac Configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule(connectionString,migrationAssembly?.FullName));
    });
    #endregion

    #region Service Collection Dependency Injections
    builder.Services.AddKeyedScoped<IProduct, Product>("Config1");
    builder.Services.AddKeyedScoped<IProduct, Product2>("Config2");
    #endregion
    // Serilog config
    builder.Host.UseSerilog((context, lc) => 
     lc.MinimumLevel.Debug()
     .MinimumLevel.Override("Microsoft" , LogEventLevel.Warning)
     .Enrich.FromLogContext()
     .ReadFrom.Configuration(builder.Configuration)
     );
    #region Mediator
    builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(BookAddCommand).Assembly));
    #endregion
    #region Docker IP Correction
    builder.WebHost.UseUrls("http://*:80");
    #endregion
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    //builder.Services.AddSingleton<IItem, Item>();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    
    app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
       .WithStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();

    app.Run();
}catch (Exception ex)
{
    Log.Fatal(ex, "App crashed");
}
finally
{
    Log.CloseAndFlush();
}
