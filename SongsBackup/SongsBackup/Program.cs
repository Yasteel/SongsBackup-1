namespace SongsBackup
{
    using Interfaces;
    using Models.SpotifyModels;
    using Profiles;
    using Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISpotifyService, SpotifyService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<ISongService, SongService>();
            builder.Services.AddScoped<ITableService, TableService>();
            
            builder.Services.AddHttpContextAccessor();
            
            // Add Mapping Profiles
            builder.Services.AddAutoMapper(typeof(SpotifyProfile).Assembly);
            
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