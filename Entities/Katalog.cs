using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Katalog:EntityBase
    {
        public int KurumId { get; set; }
        public string KatalogAdi { get; set; }
        public Guid SeriNo { get; set; }
        public byte[] KatalogPDF { get; set; }
    }
}
