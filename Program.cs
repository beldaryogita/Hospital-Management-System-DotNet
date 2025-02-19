using Microsoft.EntityFrameworkCore;

namespace MedicalRecordsManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<HospitalContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalContext")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            
            //add session
            //builder.Services.AddSession();//default with no options

            //with options
            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(10); //session timeout
            //    options.Cookie.HttpOnly = true; //whether client side script can access cookie. false means yes
            //    options.Cookie.IsEssential = true; //cookie will be always created if true
            //});

            app.UseAuthorization();

            // Add the default route mapping
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
