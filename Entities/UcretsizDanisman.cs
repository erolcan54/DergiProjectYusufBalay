using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UcretsizDanisman:EntityBase
    {
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string? Email { get; set; }
        public int Sinif { get; set; }
        public int ilId { get; set; }
        public int ilceId { get; set; }
        public bool OkulMu { get; set; }
        public bool KursMu { get; set; }
        public bool KVKK { get; set; }
    }
}
