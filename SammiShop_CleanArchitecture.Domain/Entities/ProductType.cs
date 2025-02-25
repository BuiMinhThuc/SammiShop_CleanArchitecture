using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class ProductType : BaseEntity<Guid>
    {
        public string TypeName { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
