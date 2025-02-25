using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string KeyRole { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
