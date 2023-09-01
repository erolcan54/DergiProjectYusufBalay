using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class KurumYorum:EntityBase
    {
        public int KurumId { get; set; }
        public string AdSoyad { get; set; }
        public string Yorum { get; set; }

    }
}
