using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class indirim : EntityBase
    {
        public int OkulId { get; set; }
        public double indirimOrani { get; set; }
        public DateTime SonTarih { get; set; }

    }
}
