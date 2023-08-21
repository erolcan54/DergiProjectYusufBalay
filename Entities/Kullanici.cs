using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Kullanici:EntityBase
    {
        public int OkulId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}
