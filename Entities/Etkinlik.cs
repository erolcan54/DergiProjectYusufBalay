using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Etkinlik:EntityBase
    {
        public int KurumId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
    }
}
