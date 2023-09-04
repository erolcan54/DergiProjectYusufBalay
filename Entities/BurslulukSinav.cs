using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BurslulukSinav : EntityBase
    {
        public int KurumId { get; set; }
        public DateTime SinavTarih { get; set; }
        public TimeSpan SinavSaat { get; set; }
        public DateTime SonBasvuruTarih { get; set; }
        public string SinavAdi { get; set; }
    }
}
