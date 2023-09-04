using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BurslulukSinavDisplayDto:BurslulukSinav
    {
        public string ilAdi { get; set; }
        public string ilceAdi { get; set; }
        public string KurumAdi { get; set; }
        public byte[] Resim { get; set; }
    }
}
