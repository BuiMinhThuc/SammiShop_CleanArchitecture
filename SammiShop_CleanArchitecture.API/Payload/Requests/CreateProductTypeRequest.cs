using SammiShop_CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.API.Payload.Requests
{
    public class CreateProductTypeRequest
    {
        [Required(ErrorMessage="Không được bỏ trống loại sản phẩm !")]
        [StringLength(100, ErrorMessage = "Tên loại sản phẩm không được vượt quá 100 ký tự.")]
        public string TypeName { get; set; }

    }
}
