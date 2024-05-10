using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class isBasvuru:EntityBase
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public string Telefon { get; set; }
        public byte[] CvPDF { get; set; }
        public bool KVKK { get; set; }
    }
}
