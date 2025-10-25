using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Data;
using SalesTaxAssignment.Services;
namespace SalesTaxAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesTaxAssignmentContext>(options =>
                options.UseInMemoryDatabase("SalesTaxAssignment"));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ITaxService, TaxService>();
            builder.Services.AddScoped<IReceiptService, ReceiptService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SalesTaxAssignmentContext>();
                context.Database.EnsureCreated(); // ← This forces the seed to apply
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
