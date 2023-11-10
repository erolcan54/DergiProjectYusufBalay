using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OzelDersOgretmenDto
    {
        public int Id { get; set; }
        public byte[] Resim { get; set; }
        public string AdSoyad { get; set; }
        public int BransId { get; set; }
        public string BransAdi { get; set; }
        public int ilId { get; set; }
        public string ilAdi { get; set; }
        public bool Online { get; set; }
        public bool Yuzyuze { get; set; }
        public string Hakkinda { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Status { get; set; }
        public string? TanitimVideo { get; set; }
    }
}
