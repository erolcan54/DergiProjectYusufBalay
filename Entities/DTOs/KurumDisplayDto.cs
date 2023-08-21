using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class KurumDisplayDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int ilId { get; set; }
        public string ilAdi { get; set; }
        public int ilceId { get; set; }
        public string ilceAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public int OkulTurId { get; set; }
        public string OkulTurAdi { get; set; }
        public int EgitimTurId { get; set; }
        public string EgitimTurAdi { get; set; }
        public int TikSayisi { get; set; }
        public byte[] Resim { get; set; }
    }
}
