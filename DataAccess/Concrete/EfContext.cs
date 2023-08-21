﻿using Entities;
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
            optionsBuilder.UseSqlServer("server=DESKTOP-1VTRAI5\\SQLEXPRESS;database=OkulDbProject;integrated security=true");
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
    }
}
