using Microsoft.EntityFrameworkCore;
using StationService.Business_Layer.Interfaces;
using StationService.Business_Layer.Services;
using StationService.Data;
using StationService.Interfaces;
using StationService.Services;
namespace StationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // telling what connection String to use
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            // tellling EF what context to use
            builder.Services.AddDbContext<StationeServiceContext>(options => options.UseSqlite(connectionString));

            builder.Services.AddScoped<IAdministratorRepository, AdministratorService>();

            builder.Services.AddScoped<IAssignmentRepository, AssignmentService>();

            builder.Services.AddScoped<IDispensingUnitRepository, DispensingUnitService>();

            builder.Services.AddScoped<IFuelPipeRepository, FuelPipeService>();

            builder.Services.AddScoped<IFuelQuantityRepository, FuelQuantityService>();

            builder.Services.AddScoped<IGasMeterRepository, GasMeterService>();

            builder.Services.AddScoped<IGasStationAttendantRepository, GasStationAttendantService>();

            builder.Services.AddScoped<IGasStationRepository, GasStationService>();

            builder.Services.AddScoped<ISupervisorRepository, SupervisorService>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(Program));

            // Facade service
            builder.Services.AddScoped<IAdministratorFacade, AdministratorFacade>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Administrator}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
