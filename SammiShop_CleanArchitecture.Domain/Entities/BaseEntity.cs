using System.ComponentModel.DataAnnotations;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T? Id { get; set; }
    }

}
