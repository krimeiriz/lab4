using labWork4.Core.Serializers;
using labWork4.DB;
using labWork4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace labWork4.Core
{
    public abstract class ContactRepository
    { 
        internal static int currentId = 0;
        protected readonly Dictionary<int, Contact> Contacts = new Dictionary<int, Contact>();

        protected ContactRepository(List<Contact> contacts) {
            if (contacts.Count > 0)
            {
                foreach (Contact contact in contacts)
                {
                    Contacts.Add(contact.Id, contact);
                }
                currentId = contacts.Max(c => c.Id);
            }
        }
        public static ContactRepository CreateRepository(RepositoryType type, string? path)
        {
            ISerializer serializer;
            RepositoryDBContext context;
            switch (type)
            {
                case RepositoryType.JSON:
                    serializer = new JSONSerializer(path);
                    return new SerializeBackedContactRepository(serializer);
                case RepositoryType.XML:
                    serializer = new XMLSerializer(path);
                    return new SerializeBackedContactRepository(serializer);
                case RepositoryType.DATABASE:
                    context = new RepositoryDBContext(path);
                    context.Database.EnsureCreated();
                    return new DBBackedContactRepository(context);
                default:
                    throw new ArgumentException();
            }
        }

        public virtual Task AddContact(Contact contact)
        {
            currentId++;
            contact.Id = currentId; 
            return Task.Run(()=>Contacts.Add(contact.Id, contact));
        }

        public List<Contact> GetAllContacts()
        {
            return Contacts.Values.ToList();
        }

        private List<Contact> FindContactsByPredicate(Func<Contact, bool> predicate)
        {
            /*List<Contact> resultSet = new List<Contact>();
            foreach (Contact contact in Contacts.Values)
            {
                if (predicate(contact))
                {
                    resultSet.Add(contact);
                };
            }
            return resultSet;
            */
            return Contacts.Values.Where(predicate).ToList();
        }

        public List<Contact> FindByFirstname(string firstname)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.FirstName.ToLower();
                return cLower.Contains(firstname.ToLower());
            });
        }


        public List<Contact> FindByLastname(string lastname)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.LastName.ToLower();
                return cLower.Contains(lastname.ToLower());
            });
        }

        public List<Contact> FindByFullname(string firstName, string lastname)
        {
            return FindContactsByPredicate(c =>
            {
                var fLower = c.FirstName.ToLower();
                var lLower = c.LastName.ToLower();
                return fLower.Contains(firstName.ToLower())
                        && lLower.Contains(lastname.ToLower());
            });
        }

        public List<Contact> FindByPhoneNumber(string phoneNumber)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.PhoneNumber.ToLower();
                return cLower.Contains(phoneNumber.ToLower());
            });
        }

        public List<Contact> FindByEmail(string email)
        {
            return FindContactsByPredicate(c =>
            {
                var cLower = c.Email.ToLower();
                return cLower.Contains(email.ToLower());
            });
        }

        public List<Contact> FindByAnyField(string field)
        {
            return FindContactsByPredicate(c =>
            {
                var fLower = c.FirstName.ToLower();
                var lLower = c.LastName.ToLower();
                var pLower = c.PhoneNumber.ToLower();
                var eLower = c.Email.ToLower();
                var fieldLower = field.ToLower();
                return fLower.Contains(fieldLower)
                        || lLower.Contains(fieldLower)
                        || pLower.Contains(fieldLower)
                        || eLower.Contains(fieldLower);
            });
        }

        public virtual void ResetRepository()
        {
            currentId = 0;
            Contacts.Clear();
        }
    }
}
