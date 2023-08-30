using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EtkinlikResim:EntityBase
    {
        public int EtkinlikId { get; set; }
        public byte[] Resim { get; set; }
    }
}
