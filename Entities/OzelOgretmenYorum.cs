using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OzelOgretmenYorum:EntityBase
    {
        public string AdSoyad { get; set; }
        public string Yorum { get; set; }
        public int OzelOgretmenId { get; set; }
    }
}
