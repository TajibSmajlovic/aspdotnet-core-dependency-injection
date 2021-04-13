using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TennisBookings.Web.Core.DependencyInjection;
using TennisBookings.Web.Core.Middleware;
using TennisBookings.Web.Data;

namespace TennisBookings.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppConfiguration(Configuration)
                .AddBookingServices()
                .AddBookingRules()
                .AddCourtUnavailability()
                .AddMembershipServices()
                .AddStaffServices()
                .AddCourtServices()
                .AddWeatherForecasting()
                .AddNotifications()
                .AddGreetings()
                .AddCaching()
                .AddTimeServices()
                .AddAuditing();

            //services.Configure<ExternalServicesConfig>(Configuration.GetSection("ExternalServices"));
            //services.Configure<DistributedCacheConfig>(Configuration.GetSection("DistributedCache"));
            //services.Configure<ClubConfiguration>(Configuration.GetSection("ClubSettings"));
            //services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            //services.Configure<BookingConfiguration>(Configuration.GetSection("CourtBookings"));

            //services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
            //services.AddSingleton<IWeatherForecaster, WeatherForecaster>();
            ////services.TryAddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            ////services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            ////services.RemoveAll<WeatherForecaster>();

            //// // service descriptor examples
            //// var serviceDescriptor1 = new ServiceDescriptor(typeof(IWeatherForecaster), 
            ////     typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //// var serviceDescriptor2 = new ServiceDescriptor.Describe(typeof(IWeatherForecaster), 
            ////     typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //// var serviceDescriptor3 = new ServiceDescriptor.Singleton(typeof(IWeatherForecaster), 
            ////     typeof(WeatherForecaster));
            //// var serviceDescriptor4 = new ServiceDescriptor.Singleton<IWeatherForecaster, WeatherForecaster>();
            //// services.Add(serviceDescriptor1);
            
            ////services.TryAddScoped<ILessonBookingService, LessonBookingService>();
            ////services.TryAddScoped<ILessonBookingService, LessonBookingService>();
            //services.TryAddScoped<ICourtService, CourtService>();
            //services.TryAddScoped<ICourtBookingManager, CourtBookingManager>();
            //services.TryAddScoped<IBookingService, BookingService>();
            //services.TryAddScoped<ICourtBookingService, CourtBookingService>();

            ////services.TryAddEnumerable(new[]
            ////{
            ////    ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>(),
            ////    ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>(),
            ////    ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>(),
            ////    ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>(),
            ////    ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>(),
            ////});

            ////services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>());
            ////services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>());
            ////services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>());
            ////services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>());
            ////services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>());

            //services.TryAddEnumerable(new[]
            //{
            //    ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>(),
            //    ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>(),
            //    ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>(),
            //    ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>(),
            //    ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>()
            //});

            //services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            //services.TryAddSingleton<IBookingConfiguration>(sp =>
            //    sp.GetRequiredService<IOptions<BookingConfiguration>>().Value);

            //services.TryAddSingleton<EmailNotificationService>();
            //services.TryAddSingleton<SmsNotificationService>();

            //services.AddSingleton<INotificationService>(sp =>
            //    new CompositeNotificationService(
            //        new INotificationService[]
            //        {
            //            sp.GetRequiredService<EmailNotificationService>(),
            //            sp.GetRequiredService<SmsNotificationService>()
            //        })); // composite pattern

            //services.TryAddTransient<IMembershipAdvertBuilder, MembershipAdvertBuilder>();
            //services.TryAddScoped<IMembershipAdvert>(sp =>
            //{
            //    var builder = sp.GetService<IMembershipAdvertBuilder>();

            //    builder.WithDiscount(10m);

            //    return builder.Build();
            //}); // implementation factory for complex service construction

            ////var greetingService = new GreetingService(_hostingEnvironment);
            ////services.TryAddSingleton<IHomePageGreetingService>(greetingService);
            ////services.TryAddSingleton<IGreetingService>(greetingService);

            //services.TryAddSingleton<GreetingService>();

            //services.TryAddSingleton<IHomePageGreetingService>(sp =>
            //    sp.GetRequiredService<GreetingService>());

            //services.TryAddSingleton<IGreetingService>(sp =>
            //    sp.GetRequiredService<GreetingService>());

            //services.AddDistributedMemoryCache();

            ////services.TryAddSingleton<IDistributedCache<CurrentWeatherResult>, 
            ////    DistributedCache<CurrentWeatherResult>>();

            ////services.TryAddSingleton<IDistributedCache<IEnumerable<Court>>, 
            ////    DistributedCache<IEnumerable<Court>>>();

            //services.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>)); // open generic registration

            //services.TryAddSingleton<IDistributedCacheFactory, DistributedCacheFactory>();

            //services.TryAddEnumerable(new[]
            //{
            //    ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
            //    ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
            //    ServiceDescriptor.Scoped<IUnavailabilityProvider, UpcomingHoursUnavailabilityProvider>(),
            //    ServiceDescriptor.Scoped<IUnavailabilityProvider, OutsideCourtUnavailabilityProvider>(),
            //    ServiceDescriptor.Scoped<IUnavailabilityProvider, CourtBookingUnavailabilityProvider>()
            //});

            //services.AddSingleton<TimeService>();
            //services.AddSingleton<ITimeService>(x => x.GetService<TimeService>());
            //services.AddSingleton<IUtcTimeService>(x => x.GetService<TimeService>());

            //services.AddScoped(typeof(IAuditor<>), typeof(Auditor<>));

            ////MISSING services.AddSingleton<IStaffRolesOptionsService, StaffService>();
            ////MISSING services.AddSingleton<IStaffHolidayManager, StaffHolidayManager>();
            ////MISSING services.AddSingleton<IStaffShiftManager, StaffShiftManager>();
            //services.AddSingleton<IStaffRolesOptionsService, StaffRolesOptionsService>();

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
