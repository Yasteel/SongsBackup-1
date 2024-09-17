using Microsoft.AspNetCore.Http.Features;

namespace SongsBackup
{
    using SongsBackup.Interfaces;
    using SongsBackup.Models.SpotifyModels;
    using SongsBackup.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ISpotifyService, SpotifyService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IBlobService, BlobService>();
            builder.Services.AddScoped<ITableService, TableService>();
            
            builder.Services.AddHttpContextAccessor();
            
            // allows for multipart form data with a limit of 100MB
            // builder.Services.Configure<FormOptions>(options =>
            // {
            //     options.MultipartBodyLengthLimit = 104857600;
            // });
            
            builder.Services.AddHttpClient("SpotifyClient", client =>
            {
                client.BaseAddress = new Uri(SpotifyConstants.AuthUri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(60);
                opt.Cookie.Name = ".Spotify.Credentials";
                opt.Cookie.HttpOnly = false;
                opt.Cookie.IsEssential = true;
            });

            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHttpLogging();

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();
            app.UseSession();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Landing}/{action=Connect}/{id?}");

            app.Run();
        }
    }
}