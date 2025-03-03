using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SammiShop_CleanArchitecture.Application.Interfaces;
using SammiShop_CleanArchitecture.Application.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Responsi;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Infrastructure.Cloudinary;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using SammiShop_CleanArchitecture.Infrastructure.EmailTo;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork;
using SammiShop_CleanArchitecture.Persistence.Payload.Mappers.AutoMappers;
using SammiShop_CleanArchitecture.Persistence.Services;
using SammiShop_CleanArchitecture.Persistence.Services.ProductTyoeService;
using System.Text;


namespace SammiShop_CleanArchitecture.API.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }, ServiceLifetime.Scoped);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Làm theo mẫu này. Example: Bearer {Token} ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("AppSettings:SecretKey").Value!))
                };
            });

            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var cloudinaryConfig = configuration.GetSection("CloudinaryConfiguration").Get<CloudinaryConfig>();
            services.AddSingleton(cloudinaryConfig);

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();


            services.AddAutoMapper(typeof(MappingProfile<ProductType, ProductTypeDTO>).Assembly);


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITrademarkService, TrademarkService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICardService, CardService>();


            services.AddScoped<IBaseService<ProductType>, BaseService<ProductType>>();
            services.AddScoped<IBaseService<ConfirmEmail>, BaseService<ConfirmEmail>>();
            services.AddScoped<IBaseService<Product>, BaseService<Product>>();
            services.AddScoped<IBaseService<Role>, BaseService<Role>>();
            services.AddScoped<IBaseService<Trademark>, BaseService<Trademark>>();
            services.AddScoped<IBaseService<User>, BaseService<User>>();
            services.AddScoped<IBaseService<Card>, BaseService<Card>>();

            services.AddScoped<ResponseObject<CardDTO>>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
    }
}
