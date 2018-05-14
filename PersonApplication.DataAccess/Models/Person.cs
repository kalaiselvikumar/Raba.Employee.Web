using System;
using System.ComponentModel.DataAnnotations;
using PersonApplication.DataAccess.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonApplication.DataAccess.Models
{
    public class Person : EntityBase,ISoftDelete
    {
        [Key]
        public int PersonId { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get ; set; }
        [Index("DeletedByIndex")]
        public int? DeletedBy { get; set; }
      
        public Address PersonAddress { get; set; }

    }
}
