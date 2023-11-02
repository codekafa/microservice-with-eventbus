namespace Data.Domain.Base
{
    public class BaseEntity
    {

        
        public long Id { get; set; }

        public long CreateBy { get; set; }

        public long? UpdateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
