using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labWork3Tests.Core;
using labWork4.Controllers;
using labWork4.Core;
using labWork4.Core.Serializers;
using labWork4.Models;
using Microsoft.AspNetCore.Builder;
using Moq;

namespace labWork4Tests.Controllers
{
    public class ContactsControllerTest
    {
        List<Contact> contacts()
        {
            var c = new List<Contact>();
            c.Add(new Contact
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Morgan",
                PhoneNumber = "777",
                Email = "a@a"
            }) ;
            c.Add(new Contact
            {
                Id = 2,
                FirstName = "Li",
                LastName = "Cassedey",
                PhoneNumber = "888",
                Email = "b@b"
            });
            c.Add(new Contact
            {
                Id = 3,
                FirstName = "Jack",
                LastName = "Jones",
                PhoneNumber = "999",
                Email = "c@c"
            });
            return c;
        }
      

        [Fact]
        public async void AddContact_Add_1_Contact()
        {
            var repo = new Mock<ContactRepository>();
            var controller = new ContactsController(repo.Object);

            var result = await controller.AddContact(new Mock<Contact>().Object);

            repo.Verify(r => r.AddContact(It.IsAny<Contact>()), Times.Once());            
        }
    
        [Fact]
        public async void GetAllContactsTest()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.GetAllContacts()).Returns(contacts());
            var controller = new ContactsController(repo.Object);

            var result = await controller.GetAllContacts();
            
            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Equal(3, resultSet.Count);
        }

        [Fact]
        public async void FindByFirstname()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByFirstname("Alex"))
                .Returns(contacts().Where(c=>c.FirstName.Equals("Alex")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByFirstname("Alex");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

        [Fact]
        public async void FindByLastname()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByLastname("Morgan"))
                .Returns(contacts().Where(c => c.LastName.Equals("Morgan")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByLastname("Morgan");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

        [Fact]
        public async void FindByFullname()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByFullname("Alex", "Morgan"))
                .Returns(contacts().Where(c => c.LastName.Equals("Morgan")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByFullname("Alex","Morgan");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

        [Fact]
        public async void FindByPhoneNumber()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByPhoneNumber("77"))
                .Returns(contacts().Where(c => c.PhoneNumber.Contains("77")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByPhoneNumber("77");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

        [Fact]
        public async void FindByEmail()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByEmail("a@a"))
                .Returns(contacts().Where(c => c.Email.Equals("a@a")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByEmail("a@a");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

        [Fact]
        public async void FindByAnyField()
        {
            var repo = new Mock<ContactRepository>();
            repo.Setup(r => r.FindByAnyField("Li"))
                .Returns(contacts().Where(c => c.FirstName.Equals("Li")).ToList());
            var controller = new ContactsController(repo.Object);

            var result = await controller.FindByAnyField("Li");

            var resultSet = Assert.IsAssignableFrom<List<Contact>>(result);
            Assert.Single(resultSet);
        }

    }
}
