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
            : base(serializer.Deserialize())
        {
            this._serializer = serializer;
        }

        public override void AddContact(Contact contact)
        {
            base.AddContact(contact);
            Save();
        }
        public override void ResetRepository()
        {
            base.ResetRepository();
            Save();
        }

        private void Save()
        {
            _serializer.Serialize(base.Contacts.Values.ToList());
        }
    }
}
