using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class KurumBeniAraDto:KurumBeniAra
    {
        public string KurumAdi { get; set; }
        public string ilAdi { get; set; }
        public string ilceAdi { get; set; }
    }
}
