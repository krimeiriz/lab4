using labWork4.Core.Serializers;
using labWork4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork4.Core
{
    public class SerializeBackedContactRepository : ContactRepository
    {
        private ISerializer _serializer;
        public SerializeBackedContactRepository(ISerializer serializer)
            : base(serializer.Deserialize().Result)
        {
            this._serializer = serializer;
        }

        public override Task AddContact(Contact contact)
        {
            base.AddContact(contact);
            return Save();
        }
        public async override void ResetRepository()
        {
            base.ResetRepository();
            await Save();
        }

        private Task Save()
        {
            return _serializer.Serialize(base.Contacts.Values.ToList());
        }
    }
}
