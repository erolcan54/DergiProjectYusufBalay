namespace Entities
{
    public class Okul : EntityBase
    {
        public string Ad { get; set; }
        public int ilId { get; set; }
        public int ilceId { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public int OkulTurId { get; set; }
        public int EgitimTurId { get; set; }
        public int TikSayisi { get; set; }
        public byte[] Resim { get; set; }
        public string? TanitimVideo { get; set; }
        public string? Hakkinda { get; set; }
        public string? Whatsapp { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }

    }
}
