using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OzelDersVeliBasvuru:EntityBase
    {
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Sinif { get; set; }
        public bool Online { get; set; }
        public bool Yuzyuze { get; set; }
        public int OzelDersOgretmenId { get; set; }

    }
}
