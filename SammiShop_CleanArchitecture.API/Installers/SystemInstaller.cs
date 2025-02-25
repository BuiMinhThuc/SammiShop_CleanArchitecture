using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.API.Payload.DTOs;
using SammiShop_CleanArchitecture.Application.Payload.Response;
using SammiShop_CleanArchitecture.Application.Services.ProductService;
using SammiShop_CleanArchitecture.Application.Services.ProductTyoeService;
using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Infrastructure.Data;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;
using System;

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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IBaseReponsetory<ProductType>, BaseRepository<ProductType>>();



            services.AddScoped<ResponseObject<ProductTypeDTO>>();



            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
    }
}
