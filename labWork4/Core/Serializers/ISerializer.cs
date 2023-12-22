using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labWork4.Models;

namespace labWork4.Core.Serializers
{
    public interface ISerializer
    {
        IList<Contact> Deserialize();

        void Serialize(IList<Contact> contacts);
    }
}
