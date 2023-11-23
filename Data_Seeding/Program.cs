// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new ApplicationDbContext();
Console.WriteLine("Hello, World!");




//Seed Datalar mifrationların dışında eklenmesi ve değiştirilmesi beklenmeyen durumlar için kullanılan bir özelliktir.

 


#region Data Seeding Nedir?
/*
 * EF COre ile inşa edilen veritabanı içerisinde veri tabanı nesneleri olabileceği gibi verilerin de migrate sürecinde üretilmesini isteyebiliriz.
 * işte bu ihtiyaca istinaden seed data özelliği ile EF Core üzerinden migrationlarda veriler oluşturabilir be migrate ederken bu verileri 
 * hedef tablolara basabiliriz.
 * 
 * Seed Data'lar migrate süreçlerinde gazı verileri tablolara basabilmek içi nbunları yazılım kımsında tutmamızı gerekmektedirler.
 * Böylece bu veriler üzerinde veritabanı seviyesinde istenilen manipülasyonlar rahatlıkla gerçeklestirilebilmektedir.
 * 
 * Data seed özelliği;
 * Test için geçiçi verilere ihtiyaç duyuldugunda
 * Asp.NET Core'da Identity yapılandırmasındaki roller gibi static değerler de tutalabilir.
 * Yazılım için temel konfigürasyonel değerler.
 */
#endregion
#region Seed Data Ekleme
//OnModelCreating metdo içerisinde Entity fonksiyonunda sonra çağırılan HasData fonksiyonu ilgili entitye karşılık Seed Data'ları 
//eklememizi sağlayan bir fonksiyondur.

//Pk değerlerinin manuel olarak verilmesi gerekmektedir.
//sebebi ise ilişkisel verileri de seed data ile ekleyebilmek için.
#endregion

#region İlişkisel tablolarda Seed Data
//İlişkisel senaryolarda dependent table'a veri eklerken foreing key kolonunun propertysi varsa eğer ona ilişkisel değerini vererek ekleme işlemini yapıyoruz

#endregion

#region Seed Datanın Primary Key değerini değiştirme
//Eğer ki migrate edilen herhangi bir seed datanın sonrasında PK'i değiştirillirse bu datayla ilişkisel olan veriler varsa onlara cascade davranışı gercekleşilir
#endregion






public class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
}
public class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts{ get; set; }
    public DbSet<Blog> Blogs{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasData(new Blog
        {
            Id = 1,
            Url="Deneme"
        }, new Blog
        {
            Id = 2,
            Url = "Deneme2"
        });

        modelBuilder.Entity<Post>().HasData(new Post {BlogId=1,Title="A",Content="..."  },new Post { });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}