using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Bill : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        public decimal? TotalPrice { get; set; }

        public virtual ICollection<HistorryPay>? HistorryPays { get; set; }
        public virtual User? User { get; set; }
    }
}
