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

        public async override Task<Contact> AddContact(Contact contact)
        {
            await base.AddContact(contact);
            _context.Contacts.Add(contact);
            return await _context.SaveChangesAsync() == 1 ? contact : null!;
           
        }
        public async override void ResetRepository()
        {
            base.ResetRepository();
            var tempList = new List<Contact>();
            tempList.AddRange(_context.Contacts.ToList());
            foreach(Contact c in tempList)
            {
                _context.Contacts.Remove(c);
            }
            await _context.SaveChangesAsync();
        }
    }
}
