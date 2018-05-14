using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonApplication.Controllers;
using Moq;
using PersonApplication.DataAccess.Repository;
using PersonApplication.DataAccess.Models;
using System.Data.Entity;
using System.Web.Mvc;
using PersonApplication.ViewModel;

namespace PersonApplication.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        [TestMethod]
        public void Test_EmployeeDetails()
        {
            // var _persons = _personRepository.GetAll();
            // Arrange
            var mockDbContext = new  Mock<DbContext>();
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockAddressRepository = new Mock<IAddressRepository>();

            var personList = new List<Person>();

            var Person = new Person();
            var Address = new Address();
            Address.Address1 = "23423 raja st";
            Address.Address2 = "apt# 23232";
            Address.City = "austin";
            Address.State = "tx";
            Address.ZipCode = "78717";
            Address.AddressId = 1;

            Person.PersonId = 1;
            Person.FirstName = "kalaiselvi";
            Person.LastName = "kumar";
            Person.Email = "kumar@gmail.com";
            Person.PersonAddress = Address;
            personList.Add(Person);
            
            mockPersonRepository.Setup(x => x.GetAll())
               .Returns(personList.AsQueryable());
            

            var controller = new PersonController(mockPersonRepository.Object, mockAddressRepository.Object, mockDbContext.Object);
          
            // Act
            var result = controller.EmployeeDetails() as ViewResult;

            // Assert

            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "EmployeeDetails");
         
        }



        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Test_Exception_EmployeeDetails()
        {
           
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockAddressRepository = new Mock<IAddressRepository>();

            List<Person> personList = null;

           
            mockPersonRepository.Setup(x => x.GetAll())
               .Returns(personList.AsQueryable());

          
            var controller = new PersonController(mockPersonRepository.Object, mockAddressRepository.Object, mockDbContext.Object);
         

            // Act
            var result = controller.EmployeeDetails() as ViewResult;

            // Assert

         
        }

        [TestMethod]
        public void Test_Positive_SaveEmployee()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockAddressRepository = new Mock<IAddressRepository>();

            var personList = new List<Person>();
            var Person = new Person();
            var Address = new Address();
            Address.Address1 = "23423 raja st";
            Address.Address2 = "apt# 23232";
            Address.City = "austin";
            Address.State = "tx";
            Address.ZipCode = "78717";
            Address.AddressId = 1;

            Person.PersonId = 1;
            Person.FirstName = "kalaiselvi";
            Person.LastName = "kumar";
            Person.Email = "kumar@gmail.com";
            Person.PersonAddress = Address;
            personList.Add(Person);

            var personAddressViewModel = new PersonAddressViewModel();
            personAddressViewModel.PersonId = 1;
            personAddressViewModel.FirstName = "kalaiselvi";
            personAddressViewModel.LastName = "kumar";
            personAddressViewModel.Email = "kumar@gmail.com";
            personAddressViewModel.Address1 = "23423 raja st";
            personAddressViewModel.Address2 = "apt# 23232";
            personAddressViewModel.City = "austin";
            personAddressViewModel.State = "tx";
            personAddressViewModel.ZipCode = "78717";


            mockPersonRepository.Setup(x => x.Add(Person))
             .Returns(Person);
            mockPersonRepository.Setup(x => x.Save());
            mockAddressRepository.Setup(x => x.Add(Address))
             .Returns(Address);
            mockAddressRepository.Setup(x => x.Save());

            var controller = new PersonController(mockPersonRepository.Object, mockAddressRepository.Object, mockDbContext.Object);
           

            // Act
            var result = controller.SaveEmployee(personAddressViewModel) as RedirectToRouteResult;

            // Assert

            Assert.AreEqual("EmployeeDetails", result.RouteValues["action"]);

        }

        [TestMethod]
        public void Test_Negative_SaveEmployee()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockAddressRepository = new Mock<IAddressRepository>();

            var personList = new List<Person>();
            var Person = new Person();
            var Address = new Address();
           
            var personAddressViewModel = new PersonAddressViewModel();
           
            mockPersonRepository.Setup(x => x.Add(Person))
             .Returns(Person);
            mockPersonRepository.Setup(x => x.Save());
            mockAddressRepository.Setup(x => x.Add(Address))
             .Returns(Address);
            mockAddressRepository.Setup(x => x.Save());

            var controller = new PersonController(mockPersonRepository.Object, mockAddressRepository.Object, mockDbContext.Object);
           
            controller.ModelState.AddModelError("key", "error message");

            // Act
            var result = controller.SaveEmployee(personAddressViewModel) as ViewResult;

            // Assert

            Assert.AreEqual("AddAddress", result.ViewName);

        }

    }
}
