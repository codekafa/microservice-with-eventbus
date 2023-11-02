using Data.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain
{
    public class Model : BaseEntity
    {
        [MaxLength(200)]
        public string ModelName { get; set; }
        public long BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}