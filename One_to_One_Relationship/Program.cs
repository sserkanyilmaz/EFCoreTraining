
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();
#region Default Convention
//Her iki entity'de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir

//One to One ilişki türünde dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir.
//Bu
//durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.
//Böylece foreign key'e karşılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }

//}
//public class CalisanAdresi
//{
//    public int Id { get; set;  }
//    public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }

//}
#endregion

#region Data Annotions
//Navigation propertyler tanımlanmalıdır
//FOreign Key kolonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda değildir.
//1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunun hem foreign key hem de primart key olarak
// kullanmayı tercih edilmeli ve bu duruma özen gösterilmelidir
//public class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }

//}
//public class CalisanAdresi
//{
//    [Key,ForeignKey(nameof(Calisan))]
//    public int Id { get; set; }

//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }

//}

#endregion
#region Fluent API
//Navigation propert'ler tanımlanmalı
//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içersinde OnModelCreating fonksiyonunun override edilerek mototlar aracılığıyla tasarlanması gerekmektedir. 
//Yani tüm sorumluluk bu fonksiyonun içerisinde çalışmalardadır.
public class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }

}
public class CalisanAdresi {
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }

}
#endregion
public class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar{ get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ESirketDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    //Model'ların (entity) veritabanında generate edilecek yapılarının konfigurastonları bu fonksiyon içerisinde konfigure edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c=>c.Id);
    }

}