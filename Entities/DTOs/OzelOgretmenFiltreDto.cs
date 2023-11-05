using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OzelOgretmenFiltreDto
    {
        public int ilId { get; set; }
        public int BransId { get; set; }
        public bool OnlineSecenek { get; set; }
        public bool YuzyuzeSecenek { get; set; }
    }
}
