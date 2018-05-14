using System;

namespace PersonApplication.DataAccess.Repository
{
    public interface ISoftDelete
    {
        Boolean IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        int? DeletedBy { get; set; }
    }
}
