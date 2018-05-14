using System.Data.Entity;
using PersonApplication.DataAccess.Models;


namespace PersonApplication.DataAccess.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }
        
    }
}
