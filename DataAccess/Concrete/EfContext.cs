using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=DESKTOP-1VTRAI5\\SQLEXPRESS;database=OkulDbProject;integrated security=true");
            optionsBuilder.UseSqlServer(@"Server=89.252.180.91\MSSQLSERVER2016;Database=OkulDbProject;user id=erolcann_dergi;password=?Xvb8352z;Trusted_Connection=true;Integrated Security=false");
        }

        public DbSet<il> il { get; set; }
        public DbSet<ilce> ilce { get; set; }
        public DbSet<Yonetici> Yonetici { get; set; }
        public DbSet<Okul> Okul { get; set; }
        public DbSet<OkulTur> OkulTur { get; set; }
        public DbSet<Ogretmen> Ogretmen { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Brans> Brans { get; set; }
        public DbSet<BurslulukSinav> BurslulukSinav { get; set; }
        public DbSet<indirim> indirim { get; set; }
        public DbSet<EgitimTur> EgitimTur { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<OzelDersOgretmen> OzelDersOgretmen { get; set; }
        public DbSet<OzelDersVeliBasvuru> OzelDersVeliBasvuru { get; set; }
        public DbSet<OzelOgretmenYorum> OzelOgretmenYorum { get; set; }
        public DbSet<OzelOgretmenYorumBegeni> OzelOgretmenYorumBegeni { get; set; }
        public DbSet<KurumEgitimModeli> EgitimModeli { get; set; }
        public DbSet<KurumEgitimModeliResim> EgitimModeliResim{ get; set; }
        public DbSet<Basari> Basari { get; set; }
        public DbSet<Katalog> Katalog { get; set; }
        public DbSet<IcGorsel> IcGorsel { get; set; }
        public DbSet<DisGorsel> DisGorsel { get; set; }
        public DbSet<Etkinlik> Etkinlik { get; set; }
        public DbSet<EtkinlikResim> EtkinlikResim { get; set; }
        public DbSet<Kulup> Kulup { get; set; }
        public DbSet<KurumYorum> KurumYorum { get; set; }
        public DbSet<KurumYorumBegeni> KurumYorumBegeni { get; set; }
        public DbSet<KurumBeniAra> KurumBeniAra { get; set; }
        public DbSet<BurslulukSinavBasvuru> BurslulukSinavBasvuru { get; set; }
        public DbSet<isBasvuru> isBasvuru { get; set; }

    }
}
