using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Persisting_The_Data_Update
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Veri Nasıl Güncellenir?

            #endregion
            #region ChabgeTracker Nedir? kısaca..

            #endregion
            #region Takip edilmeyen nesneler nasıl güncellenir


            #endregion
            #region EntityState Nedir

            #endregion
            #region EFCore açısından bir verinin güncellenmesi gerektiği nasıl anlaşılıyor

            #endregion
            #region Birden fazla veri eklerken nelere dikkat edilmelidir?

            #endregion
        }
    }

    public class ETicaretDbContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB;Database=ETicaretDb");
        }
    }
    public class Urun
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public float Fiyat { get; set; }

    }
}
