namespace Entities
{
    public class Ogretmen : EntityBase
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public byte[] Resim { get; set; }
        public int BransId { get; set; }
        public int KurumId { get; set; }
    }
}
