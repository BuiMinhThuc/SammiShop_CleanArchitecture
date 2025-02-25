using SammiShop_CleanArchitecture.Domain.Entities;
using SammiShop_CleanArchitecture.Infrastructure.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Application.Services.ProductService
{
    public interface IProductTypeService: IBaseReponsetory<ProductType>
    {
    }
}
