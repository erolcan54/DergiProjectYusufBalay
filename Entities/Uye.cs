using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Uye:EntityBase
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public Guid UyeGuid { get; set; }
        public string? SmsCode { get; set; }
        public bool? SmsCodeActive { get; set; }
        public string? MailCode { get; set; }
        public bool? MailCodeActive { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public bool KVKK { get; set; }
        public string Sifre { get; set; }
    }
}
