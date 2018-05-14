using PersonApplication.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PersonApplication.DataAccess
{
    public class PersonDBContext : DbContext 
    {
        public PersonDBContext() : base("name=PersonDBContext")
        {
           Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new UniDBInitializer<PersonDBContext>());

            modelBuilder.Entity<Person>()

                        .HasRequired(s => s.PersonAddress)
                        .WithRequiredPrincipal(ad => ad.Person);
            
                        
            base.OnModelCreating(modelBuilder);
        }


        private class UniDBInitializer<T> :DropCreateDatabaseIfModelChanges<PersonDBContext>
        {

            protected override void Seed(PersonDBContext context)
            {

                var personList = new List<Person>();

                personList.Add(new Person()
                {
                    PersonId = 1,
                    FirstName = "David",
                    LastName = "Steve",
                    Email = "david@gmail.com",
                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });

                personList.Add(new Person()
                {
                    PersonId = 2,
                    FirstName = "Kalai",
                    LastName = "Kumar",
                    Email = "Kumar@gmail.com",
                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });

                personList.Add(new Person()
                {
                    PersonId = 3,
                    FirstName = "raj",
                    LastName = "suresh",
                    Email = "suresh@gmail.com",
                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });

                personList.Add(new Person()
                {
                    PersonId = 4,
                    FirstName = "ram",
                    LastName = "rajesh",
                    Email = "ramsuresh@gmail.com",
                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });


                personList.Add(new Person()
                {
                    PersonId = 5,
                    FirstName = "semal",
                    LastName = "suresh",
                    Email = "semal@gmail.com",
                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });


                personList.Add(new Person()
                {
                    PersonId = 6,
                    FirstName = "nisha",
                    LastName = "suresh",
                    Email = "nisha@gmail.com",

                    DeletedBy = 0,
                    DeletedDate = DateTime.Now,
                    IsDeleted = false
                });
                
                personList.ForEach(s => context.tblPerson.AddOrUpdate(p => p.Email, s));
                context.SaveChanges();


                IList<Address> addressList = new List<Address>();
                addressList.Add(new Address()
                {
                    Address1 = "1550 Pamer Street",
                    Address2 = "Apt# 3434",
                    City = "Austin",
                    State = "Texas",
                    AddressId=1,
                    ZipCode = "78717"
                }
                 );

                addressList.Add(new Address()
                {
                    Address1 = "12 Dell Street",
                    Address2 = "Apt# 565",
                    City = "Austin",
                    State = "Texas",
                    AddressId = 2,
                    ZipCode = "78567"
                }
                 );

                addressList.Add(new Address()
                {
                    Address1 = "34534 kalai Street",
                    Address2 = "Apt# 565",
                    City = "Austin",
                    State = "Texas",
                    AddressId = 3,
                   ZipCode = "76889",
                }
                 );


                addressList.Add(new Address()
                {
                    Address1 = "34534 kalai Street",
                    Address2 = "Apt# 565",
                    City = "Austin",
                    State = "Texas",
                    AddressId = 4,
                    ZipCode = "76889",
                }
                 );



                addressList.Add(new Address()
                {
                    Address1 = "34534 kalai Street",
                    Address2 = "Apt# 565",
                    City = "Austin",
                    State = "Texas",
                    AddressId = 5,
                    ZipCode = "76889",
                }
                 );



                addressList.Add(new Address()
                {
                    Address1 = "34534 kalai Street",
                    Address2 = "Apt# 565",
                    City = "Austin",
                    State = "Texas",
                    AddressId = 6,
                    ZipCode = "76889",
                }
                 );

                foreach (Address e in addressList)
                {
                    var addressinDatabase = context.tblAddress.Where(
                        s => s.Person.PersonId== e.AddressId).SingleOrDefault();
                    if (addressinDatabase == null)
                    {
                        context.tblAddress.Add(e);
                    }
                }
                context.SaveChanges();
                base.Seed(context);
            }
        }
        public virtual DbSet<Person>  tblPerson { get; set; }
        public virtual DbSet<Address> tblAddress { get; set; }
    }
}
