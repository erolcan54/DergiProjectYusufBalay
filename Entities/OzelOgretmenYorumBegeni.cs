using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OzelOgretmenYorumBegeni:EntityBase
    {
        public string IPAddress { get; set; }
        public int YorumId { get; set; }
        public int Begeni { get; set; }
    }
}
