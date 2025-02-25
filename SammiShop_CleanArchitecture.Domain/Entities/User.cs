using SammiShop_CleanArchitecture.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UrlAvt { get; set; }

        public string Email { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public int? RoleId { get; set; }=(int) ConstantEnum.Role.Member;
        public virtual Role? Role { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<ComfirmEmail>? ComfirmEmails { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<Card>? Cards { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }


      
    }
}
