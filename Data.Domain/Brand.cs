using Data.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain
{
    public class Brand : BaseEntity
    {
        [MaxLength(200)]
        public string BrandName { get; set; }

    }
}