
using PersonApplication.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace PersonApplication.DataAccess.Repository
{
    public interface IPersonRepository : IGenericRepository <Person>
    {
        new IQueryable<Person> GetAll();

    }
}
