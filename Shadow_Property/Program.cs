// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();


#region Shadow Properties - Gölge Özellikler
//Entity sınıflarında fiziksel olarak tanımlanmayan/modellenmeyen ancak EF Core tarafından ilgili
//entity için var olan/var olduğu kabul edilen property'lerdir

//Tabloda gösterilmesini istemediğimiz/lüzumlu görmediğimiz/entity instance'ı üzerinde işlem yapmayacağımız kolonlar için shadow property'ler 
//kullanılabilir

//Shadow property'lerin değerleri ve stateleri Chane Tracker tarafından kontrol edilir.
#endregion

#region Foreign Key - Shadow Properties
//İlişkisel senaryolarda foreign key property'sini tanımlamadığımız halde EF Core tarafından dependent entity'e
//eklenmektedir. İşte bu Shadow Property'dir.
//var blogs = await context.Blogs.Include(b=>b.Posts).ToListAsync();
//Console.WriteLine(  "sd");
#endregion
#region Shadow Property Oluşturma
//Bir entity üzerinde Shadow Property oluşturmak istiyorsanız eğer Fluent API kullanmanız gerekmektedir.
/*
 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");
    }
 */
#endregion
#region Shadow Property'e Erişim Sağlama

#region ChangeTracker İle Erişim

#endregion

#region ChangeTracker İle Erişim
//Shadow Property'e erişim sağlamak için ChangeTracker kullanılabilir
//var blog = await context.Blogs.FirstAsync();

//var createdDate = context.Entry(blog).Property("CreatedDate");
//Console.WriteLine(createdDate.CurrentValue);//Şuanki deger
//Console.WriteLine(createdDate.OriginalValue);//ChangeTracker'a ilk gelen deger
//createdDate.CurrentValue = DateTime.Now;//Shadow property degerini değiştirme
//await context.SaveChangesAsync();//Kaydetme
#endregion
#region EF.Property İle Erişim
//Özellikle LINQ sorgularında Shadow Property'lerine erişim için EF.Property static yapılanmasını 
//kullanabiliriz.

var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b, "CreatedDate")).ToListAsync();

var blogs2 = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();//Where sorgusunda kullanımı


#endregion
#endregion




Console.ReadLine();
public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }
    public Blog blog { get; set; }
}
public class ApplicationDbContext : DbContext

{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts{ get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");
    }
}