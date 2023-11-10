using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OzelDersOgretmen:EntityBase
    {
        public byte[] Resim { get; set; }
        public string AdSoyad { get; set; }
        public int BransId { get; set; }
        public int ilId { get; set; }
        public bool Online { get; set; }
        public bool Yuzyuze { get; set; }
        public string Hakkinda { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string? TanitimVideo { get; set; }
    }
}
