using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Blog : EntityBase
    {
        public byte[] Resim { get; set; }
        public string Baslik { get; set; }
        public string Konu { get; set; }
        public int Hit { get; set ; }
    }
}
