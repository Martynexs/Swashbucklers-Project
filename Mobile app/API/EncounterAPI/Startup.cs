using EncounterAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using AuthorizationService;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EncounterAPI.Middleware;
using Repository;
using Contracts;
using Autofac.Extras.DynamicProxy;
using Services;
using Contracts.Services;

namespace EncounterAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.ConfigureRepositoryWrapper();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EncounterAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            services.AddDbContext<EncounterContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserInfoPolicy", policy =>
                    policy.Requirements.Add(new SameUserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, RouteAuthorizationCrudHandler>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("DogIsAMan'sBestFriend")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                }
                );
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<RatingsService>().As<IRatingsService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<WaypointCompletionService>().As<IWaypointCompletionService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<RouteRepository>().As<IRouteRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<WaypointRepository>().As<IWaypointRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<UserRopository>().As<IUserRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<RatingRepository>().As<IRatingRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<QuizRepository>().As<IQuizRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<QuizAnswerRepository>().As<IQuizAnswerRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();


            builder.RegisterType<RouteCompletionRepository>().As<IRouteCompletionRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();
            

            builder.RegisterType<WaypointCompletionRepository>().As<IWaypointCompletionRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.RegisterType<ScoresService>().As<IScoresService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor))
                .InstancePerDependency();

            builder.Register(x => Log.Logger).SingleInstance();
            builder.RegisterType<LoggingInterceptor>().SingleInstance();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EncounterAPI v1"));
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseMiddleware<ErrorHandler>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
