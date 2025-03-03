using SammiShop_CleanArchitecture.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace SammiShop_CleanArchitecture.Domain.Extensions
{
    public class PaginationExtension
    {
        [Range(ConstantDomain.MIN_PAGESIZE_LENGTH, ConstantDomain.MAX_PAGESIZE_LENGTH, ErrorMessage = "Số bản ghi muốn lấy không hợp lệ !")]
        public int PageSize { get; set; } = ConstantDomain.MIN_PAGESIZE_LENGTH;

        private int _pageNumber = ConstantDomain.MIN_PAGENUMBER_LENGTH;
        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                if (value < ConstantDomain.MIN_PAGENUMBER_LENGTH)
                    throw new ArgumentOutOfRangeException(nameof(PageNumber), "PageNumber must be greater than or equal to the minimum value.");
                _pageNumber = value;
            }
        }
    }

}
