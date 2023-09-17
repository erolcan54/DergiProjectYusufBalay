using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class isBasvuru:EntityBase
    {
        public string AdSoyad { get; set; }
        public int ilId { get; set; }
        public int BransId { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public byte[] Resim { get; set; }
        public string Hakkinda { get; set; }
    }
}
