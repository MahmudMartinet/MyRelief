using System;

namespace Relief.Contracts
{
    public interface ISoftDelete
    {
        public DateTime DeletedOn { get; set; }
        public int DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
