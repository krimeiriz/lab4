using labWork4.Core.Serializers;
using labWork4.DB;
using labWork4.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork4.Core
{
    public class DBBackedContactRepository : ContactRepository
    {
        private RepositoryDBContext _context;
        public DBBackedContactRepository(RepositoryDBContext context)
            : base(context.Contacts.ToList())
        {
            this._context = context;
        }

        public override void AddContact(Contact contact)
        {
            base.AddContact(contact);
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }
        public override void ResetRepository()
        {
            base.ResetRepository();
            var tempList = new List<Contact>();
            tempList.AddRange(_context.Contacts.ToList());
            foreach(Contact c in tempList)
            {
                _context.Contacts.Remove(c);
            }
        }
    }
}
