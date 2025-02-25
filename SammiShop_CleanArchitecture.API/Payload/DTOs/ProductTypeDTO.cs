using SammiShop_CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.API.Payload.DTOs
{
    public class ProductTypeDTO
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }

    }
}
