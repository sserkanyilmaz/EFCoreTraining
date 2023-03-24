using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine(  "werdfgh");
#region Default Convention
//İki entity arasındaki ilişkiyi navigation propertyler üzreinden çoğul olarak kurmalıyız.
//(ICollection, List).

//Default convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Core tasarımına uygun bir şekilde cross table'ı otomatik olarak basacak ve generate edecektir.
//ve de oluşturulan cross table'ın içerisinde composite primary key'i de otomatik olarak oluşturulmuş olacaktır.
//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }
//}
//public class Yazar
//{
//    public int Id { get; set; } 
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}


#endregion

#region Data Annotations 
//Cross table manuel olarak oluşturulmak zorundadır.
//Entity'leri oluşturduğumuz cross table entitiysi ile bire çok bir ilişki kurulmalı.
//Cross Table'da composite primary key'i data annotation(Attributes)kar ile manuel kuramıyoruz. bunun için de Fluent API'da çalışma yapmamız gerekiyor.
//Cross Table'a karşılık bir entity modeli oluşturuyorsak eğer bunu context sınıfı içerisinde DbSet propertysi ile bildirmek zorunda değiliz.

//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}
//public class KitapYazar
//{
//    [ForeignKey(nameof(Kitap))]
//    public int KId { get; set; }

//    [ForeignKey(nameof(Yazar))]
//    public int YId { get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}
//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar{ get; set; }
//}
#endregion

#region Fluent API
//Cross Table manuel oluşturulmalı.
//Dbset olarak eklenmesine gerek yok
//Composite PK Haskey metodu ile kurulmalı

public class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }
}
public class KitapYazar
{
    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}
public class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}
#endregion










public class EKitapDbContext : DbContext
{
    public DbSet<Yazar> Yazarlar { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TKitapDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected  override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>().HasKey(ky => new
        {
            ky.KitapId,
            ky.YazarId,
        });
        modelBuilder.Entity<KitapYazar>().HasOne(ky => ky.Kitap).WithMany(k => k.Yazarlar).HasForeignKey(ky=>ky.KitapId);
        modelBuilder.Entity<KitapYazar>().HasOne(ky=>ky.Yazar).WithMany(k=> k.Kitaplar).HasForeignKey(ky => ky.YazarId);
    }
}
