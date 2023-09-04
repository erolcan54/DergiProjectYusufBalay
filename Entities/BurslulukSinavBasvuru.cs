using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BurslulukSinavBasvuru : EntityBase
    {
        public string VeliAdSoyad { get; set; }
        public string OgrenciAdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int SinavId { get; set; }
    }
}
