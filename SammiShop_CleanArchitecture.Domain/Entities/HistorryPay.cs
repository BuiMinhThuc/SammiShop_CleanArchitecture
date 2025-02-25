using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class HistorryPay : BaseEntity<Guid>
    {
        public Guid BillId { get; set; }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Bill? Bill { get; set; }

    }
}
