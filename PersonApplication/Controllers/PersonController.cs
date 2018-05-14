using PersonApplication.DataAccess.Models;
using System.Web.Mvc;
using PersonApplication.DataAccess.Repository;
using System.Linq;
using PersonApplication.ViewModel;
using System;
using System.Data.Entity;

namespace PersonApplication.Controllers
{
    //  [HandleError(View = "Error")]
    public class PersonController : Controller
    {
      
        private readonly IPersonRepository _personRepository = null;
        private readonly IAddressRepository _addressRepository = null;
        private readonly DbContext dbContext = null;
        public PersonController(IPersonRepository personRepository ,IAddressRepository addressRepository, DbContext _dbContext )
        {
             dbContext = _dbContext;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
        
        }

        public ActionResult EmployeeDetails()
        {
            try
            {


                var _persons = _personRepository.GetAll();
               
                var _personAddressList = (from p in _persons
                                          select new PersonAddressViewModel()
                                          {
                                              PersonId = p.PersonId,
                                              FirstName = p.FirstName,
                                              LastName = p.LastName,
                                              Email = p.Email,
                                              Address1 = p.PersonAddress.Address1,
                                              Address2 = p.PersonAddress.Address2,
                                              City = p.PersonAddress.City,
                                              State = p.PersonAddress.State,
                                              ZipCode = p.PersonAddress.ZipCode
                                          }).ToList();

                return View("EmployeeDetails",_personAddressList);
            }
            catch (Exception ex)
            {
                throw ex;
            
            }
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult AddEmployee(PersonAddressViewModel PersonAddressviewodel)
        {
          return  RedirectToAction("AddAddress", PersonAddressviewodel);
        }

       
        public ActionResult AddAddress(PersonAddressViewModel personAddressViewModel)
        {
            return View("AddAddress", personAddressViewModel);
         }

        
        public ActionResult EditEmployee(PersonAddressViewModel personAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                var Person = new Person();
                var Address = new Address();

                Person.FirstName = personAddressViewModel.FirstName;
                Person.LastName = personAddressViewModel.LastName;
                Person.Email = personAddressViewModel.Email;
                Address.Address1 = personAddressViewModel.Address1;
                Address.Address2 = personAddressViewModel.Address2;
                Address.City = personAddressViewModel.City;
                Address.State = personAddressViewModel.State;
                Address.ZipCode = personAddressViewModel.ZipCode;
                Address.AddressId = personAddressViewModel.PersonId;
              
                _addressRepository.Edit(Address);
                _addressRepository.Save();
                return RedirectToAction("EmployeeDetails");
            }
            else
            {
                return View("EditEmployee", personAddressViewModel);
            }
        }

        public ActionResult SaveEmployee(PersonAddressViewModel personAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                var Person = new Person();
                var Address = new Address();

                Person.FirstName = personAddressViewModel.FirstName;
                Person.LastName = personAddressViewModel.LastName;
                Person.Email = personAddressViewModel.Email;
                Address.Address1 = personAddressViewModel.Address1;
                Address.Address2 = personAddressViewModel.Address2;
                Address.City = personAddressViewModel.City;
                Address.State = personAddressViewModel.State;
                Address.ZipCode = personAddressViewModel.ZipCode;

                _personRepository.Add(Person);
                _personRepository.Save();

                Address.AddressId = Person.PersonId;
                _addressRepository.Add(Address);
                _addressRepository.Save();
                return RedirectToAction("EmployeeDetails");
            }
            else
            {
                return View("AddAddress", personAddressViewModel);
            }
        }
        public ActionResult Edit(int Id)
        {
            var p = _personRepository.GetAll().Where(x => x.PersonId == Id).Single();

               var model = new PersonAddressViewModel()
                {
                             PersonId = p.PersonId,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             Email = p.Email,
                             Address1 = p.PersonAddress.Address1,
                             Address2 = p.PersonAddress.Address2,
                             City = p.PersonAddress.City,
                             State = p.PersonAddress.State,
                             ZipCode = p.PersonAddress.ZipCode
                };
            return View("EditEmployee",model);
        }

       
        
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var Person = _personRepository.GetById(Id);
            var Address = _addressRepository.GetById(Id);
            _personRepository.Delete(Person);
            _personRepository.Save();

            Address.AddressId = Person.PersonId;
            _addressRepository.Delete(Address);
            _personRepository.Save();

            return RedirectToAction("EmployeeDetails");
        }
      
    }
   
}