using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Application.Domain
{
    public class PageRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
