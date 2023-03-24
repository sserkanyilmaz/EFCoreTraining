using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("asdfghjfd");
#region Default Convention
//Default convention yönteminde bire çok ilişkiyi kurarken Foregin key kolonuna karşılık gelen bir property tanımlamak mecburiyetinde değiliz.
//Eğer tanımlamazsak Ef Core bunu kendisi tamamlayacaktır. Eğer tanımlarsak tanımladığımızı baz alacaktır.
//public class Calisan //Dependent Entity
//{
//    public int Id { get; set; }
//    public int DepartmanId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }

//}

//public class Departman {
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }


//}
#endregion
#region Data Annotations 
//Default Convention yönteminde foreign key kolonuna karşılık gelen property'i tanımladığımızda bu property ismi temel genleneksel entity
//tanımalama kurallarına uymuyorsa eğer Data Annotations'lar ile müdahalede bulunabiliriz.
//public class Calisan //Dependent Entity
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int IstegeBaglıForeignKeyIsmi { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }

//}
//public class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }
//}
#endregion
#region Fluent API
public class Calisan //Dependent Entity
{
    public int Id { get; set; }
    public int DId { get; set; }
    public string Adi { get; set; }
    public Departman Departman { get; set; }

}
public class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}
#endregion
public class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar{ get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ESirketDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    //Model'ların (entity) veritabanında generate edilecek yapılarının konfigurastonları bu fonksiyon içerisinde konfigure edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>().
            HasOne(c => c.Departman).
            WithMany(c => c.Calisanlar).
            HasForeignKey(c=>c.DId);
    }

}