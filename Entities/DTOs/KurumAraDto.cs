using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class KurumAraDto
    {
        public int il1 { get; set; }
        public int ilce1 { get; set; }
        public int okulTurId { get; set; }
        public int il2 { get; set; }
        public int ilce2 { get; set; }
        public bool OkulArama { get; set; }
        public bool KursArama { get; set; }
        public bool KurumYorumSecenekOkul { get; set; }
        public bool KurumYorumSecenekKurs { get; set; }
    }
}
