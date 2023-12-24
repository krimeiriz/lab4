using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using labWork4.Core;
using labWork4.Core.Serializers;
using labWork4.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace labWork3Tests.Core
{
    public class SerializeBackedContactRepositoryTest
        : BaseContactRepositoryTest<SerializeBackedContactRepository>,
        IDisposable
    {
        List<Contact> mockSerializerList = new List<Contact>();
        ISerializer _serializer;
        public SerializeBackedContactRepositoryTest()
        {
            var mockSerializer = new Mock<ISerializer>();
            mockSerializer.Setup(m => m.Serialize(It.IsAny<List<Contact>>()))
                .Callback<List<Contact>>((s) => mockSerializerList = s);

            mockSerializer.Setup(m => m.Deserialize())
                .Returns(()=> (Task.FromResult(mockSerializerList ?? new List<Contact>())));

            _serializer = mockSerializer.Object;
            this.contactRepository = new SerializeBackedContactRepository(_serializer);
        }

     


        [Fact]
        public async void Save_Save2_Contacts_To_Data_Source()
        {
            var contact1 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };
            var contact2 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };

            await contactRepository.AddContact(contact1);
            await contactRepository.AddContact(contact2);

            var actual = mockSerializerList.Count;
            Assert.Equal(2, actual);
        }

        [Fact]
        public async void Load_Load2_Contacts_From_Data_Source()
        {
            var contact1 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };
            var contact2 = new Contact { FirstName = "test", LastName = "test", PhoneNumber = "777", Email = "test" };

            await contactRepository.AddContact(contact1);
            await contactRepository.AddContact(contact2);
            contactRepository = new(_serializer);
            
            var actual = contactRepository.GetAllContacts().Count;
            Assert.Equal(2, actual);
        }
    }
}
