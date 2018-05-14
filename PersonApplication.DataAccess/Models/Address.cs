using PersonApplication.DataAccess.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PersonApplication.DataAccess.Models
{
    public class Address : EntityBase, ISoftDelete
    {
        [ForeignKey("Person")]
        public int AddressId { get; set; }
      
        public string Address1 { get; set; }
        public string Address2 { get; set; }
      
        public string City { get; set; }
      
        public string State { get; set; }
      
        public string ZipCode { get; set; }

        public virtual Person Person { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
    }
}
