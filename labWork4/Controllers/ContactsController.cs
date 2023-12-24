using labWork4.Core;
using labWork4.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
//using labWork4.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace labWork4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ContactRepository _repository;

        public ContactsController(ContactRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public Task<List<Contact>> GetAllContacts()
        {
            return Task.FromResult(_repository.GetAllContacts());
        }

        [HttpPost]
        public Task<Contact> AddContact([FromBody] Contact contact)
        {
            return _repository.AddContact(contact);

        }

        [HttpGet("find/firstname")]
        public Task<List<Contact>> FindByFirstname(
            [FromQuery(Name = "firstname")] string firstname
        )
        {
            return Task.FromResult(_repository.FindByFirstname(firstname));
        }


        [HttpGet("find/lastname")]
        public Task<List<Contact>> FindByLastname(
            [FromQuery(Name = "lastname")] string lastname
        )
        {
            return Task.FromResult(_repository.FindByLastname(lastname));
        }

        [HttpGet("find/fullname")]
        public Task<List<Contact>> FindByFullname(
           [FromQuery(Name = "firstname")] string firstname,
           [FromQuery(Name = "lastname")] string lastname)
        {
            return Task.FromResult(_repository.FindByFullname(firstname, lastname));
        }


        [HttpGet("find/phonenumber")]
        public Task<List<Contact>> FindByPhoneNumber(
             [FromQuery(Name = "phonenumber")] string phoneNumber
        )
        {
            return Task.FromResult(_repository.FindByPhoneNumber(phoneNumber));
        }

        [HttpGet("find/email")]
        public Task<List<Contact>> FindByEmail(
            [FromQuery(Name = "email")] string email
        )
        {
            return Task.FromResult(_repository.FindByEmail(email));
        }

        [HttpGet("find/anyfield")]
        public Task<List<Contact>> FindByAnyField(
           [FromQuery(Name = "field")] string field)
        {
            return Task.FromResult(_repository.FindByAnyField(field));
        }

    }
}
