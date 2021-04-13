using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Auditing;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Core.Caching;
using TennisBookings.Web.Core.Middleware;
using TennisBookings.Web.Data;
using TennisBookings.Web.Domain;
using TennisBookings.Web.Domain.Rules;
using TennisBookings.Web.External;
using TennisBookings.Web.Services;
using TennisBookings.Web.Services.Notifications;

namespace TennisBookings.Web
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ExternalServicesConfig>(Configuration.GetSection("ExternalServices"));
            services.Configure<DistributedCacheConfig>(Configuration.GetSection("DistributedCache"));
            services.Configure<ClubConfiguration>(Configuration.GetSection("ClubSettings"));
            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            services.Configure<BookingConfiguration>(Configuration.GetSection("CourtBookings"));

            services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
            services.AddSingleton<IWeatherForecaster, WeatherForecaster>();

            services.TryAddScoped<ILessonBookingService, LessonBookingService>();
            services.TryAddScoped<ICourtService, CourtService>();
            services.TryAddScoped<ICourtBookingManager, CourtBookingManager>();
            services.TryAddScoped<IBookingService, BookingService>();
            services.TryAddScoped<ICourtBookingService, CourtBookingService>();
            services.TryAddScoped<ICourtMaintenanceService, CourtMaintenanceService>();

            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>(),
                ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>(),
                ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>(),
                ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>()
            });

            services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            services.AddSingleton<INotificationService, EmailNotificationService>();

            services.TryAddTransient<IMembershipAdvertBuilder, MembershipAdvertBuilder>();
            services.TryAddScoped<IMembershipAdvert>(sp =>
            {
                var builder = sp.GetService<IMembershipAdvertBuilder>();

                builder.WithDiscount(10m);

                return builder.Build();
            });

            services.TryAddSingleton<IHomePageGreetingService, GreetingService>();

            services.AddDistributedMemoryCache();

            services.TryAddSingleton<IDistributedCacheFactory, DistributedCacheFactory>();

            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
                ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
                ServiceDescriptor.Scoped<IUnavailabilityProvider, UpcomingHoursUnavailabilityProvider>(),
                ServiceDescriptor.Scoped<IUnavailabilityProvider, OutsideCourtUnavailabilityProvider>(),
                ServiceDescriptor.Scoped<IUnavailabilityProvider, CourtBookingUnavailabilityProvider>()
            }); // register multiple implementations manually

            services.AddSingleton<TimeService>();
            services.AddSingleton<ITimeService>(x => x.GetService<TimeService>());
            services.AddSingleton<IUtcTimeService>(x => x.GetService<TimeService>());

            services.AddScoped(typeof(IAuditor<>), typeof(Auditor<>));

            //MISSING services.AddSingleton<IStaffRolesOptionsService, StaffService>();
            //MISSING services.AddSingleton<IStaffHolidayManager, StaffHolidayManager>();
            //MISSING services.AddSingleton<IStaffShiftManager, StaffShiftManager>();
            services.AddSingleton<IStaffRolesOptionsService, StaffRolesOptionsService>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {                
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/FindAvailableCourts");
                    options.Conventions.AuthorizePage("/BookCourt");
                    options.Conventions.AuthorizePage("/Bookings");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<TennisBookingsUser, TennisBookingsRole>()
                .AddEntityFrameworkStores<TennisBookingDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddDbContext<TennisBookingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseLastRequestTracking(); // only track requests which make it to MVC, i.e. not static files
            app.UseMvc();
        }
    }
}
