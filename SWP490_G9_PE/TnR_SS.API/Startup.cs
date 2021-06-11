using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Configurations;
using TnR_SS.API.Middleware.ErrorHandle;
using TnR_SS.DataEFCore;
using TnR_SS.Domain.Entities;

namespace TnR_SS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; } // to use Config from other class

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddDbContext<TnR_SSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TnR_SS")));
            /*services.AddDbContext<TnR_SSContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("TnR_SS")));*/

            services.ConfigureRepositories()
                .ConfigureSupervisor();

            services.AddIdentity<UserInfor, RoleUser>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredLength = 8;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.User.RequireUniqueEmail = false;
                cfg.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<TnR_SSContext>()
            .AddDefaultTokenProviders();

            // add cors configure
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3010")
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        //.WithHeaders(HeaderNames.ContentType);
                    });
            });

            services.AddAuthentication(
                //JwtBearerDefaults.AuthenticationScheme
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false; // truy cập mã thông báo trong controller khi cần thiết
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        RequireExpirationTime = true
                };
        });

            services.AddAutoMapper(typeof(Startup));

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // handle ModelBiding Exception
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
                {
            //options.SuppressModelStateInvalidFilter = true;

            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage).ToList();
                ResponseBuilder rpb = new ResponseBuilder().Errors(errors);

                return new BadRequestObjectResult(rpb.ResponseModel);
            };
        });

            //services.AddTransient<HandleOTP>();
            /*services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });*/
            //services.Configure<TokenGeneration>(Configuration.GetSection("Jwt"));
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SWP490_G9_PE", Version = "v1" });
            });*/
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseExceptionHandler("/api/error-local-development");
            //app.UseDeveloperExceptionPage();
            /*app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SWP490_G9_PE v1"));*/

            // exception handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
        else
        {
            app.UseExceptionHandler("/api/error");
            // exception handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }



        //HSTS

        //HttpsRedirection
        app.UseHttpsRedirection();

        //static file

        //routing
        app.UseRouting();

        //use cors
        app.UseCors();

        //response caching
        //app.UseResponseCaching();

        //authentication
        app.UseAuthentication();

        //authorization
        app.UseAuthorization();

        //custom

        //end point
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}
}
