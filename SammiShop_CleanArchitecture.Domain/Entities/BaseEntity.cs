using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T? Id { get; set; }
    }

}
