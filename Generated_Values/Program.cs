// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();
#region Generated Value Nedir?
//EF Core'da üretilen değerlerle ilgili çeşitli modellerin ayrıntılarını yapılandırmamızı sağlayan bir konfigürasyondur.
#endregion

#region Default Values
//EF Core'da herhangi bir tablonun herhangi bir kolonuna yazılım tarafından bir değer gönderilmediği takdirde bu kolona hangi değerin(default value) üretilip 
//Yazdırılacağını belirleyen yapılanmalardır.
#region HasDefaultValue
//Static değer verme
//Person p = new()
//{
//    Name = "Serkan",
//    Surname = "Microsoft",
//    Premium = 100,
//    TotalGain = 100,
//    PersonCode = 1
//};
//await context.Persons.AddAsync(p);
//await context.SaveChangesAsync();
#endregion

#region HasDefaultValueSql
//Sql cümleciği ile değer verme
#endregion
#endregion

#region ComputedColumns

#region HasComputedColumnSql
//Tablo içerisindeki kolonlar üzerinde yapılan aritmatik işlemler neticesinde üretilen kolondur.

#endregion

#endregion

#region Value Generation

#region Primary Keys
//Herhangi bir tablodaki satırları kimlik vari şekilde tanımlayan, tekil(unique) olan sütun veya sütunlardır. 
#endregion

#region Identity
//Identity, yanlızca otomatik olarak artan bir sütundur. bir sütun,PK olmaksızın identity olarak tanımlanabilir
//Bir tablo içersinde identity kolonu sadece tek bir tane olarak kullanılabilir
#endregion
//Bu iki özellik genellikle birlikte kullanılmaktadırlar. o yüzden EF Core PK olan bir kolonu otomatik olarak Identity olacak şekilde yapılandırmaktadır.
//Ancak böyle olması için bir gereklilik yoktur!
#region DatabaseGenerated

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse None ile işaretliyoruz.
//Ef Core'un default olarak PK kolonlarda getirdiği Identity özelliğini kaldırmak istiyorsak
//eğer None'ı kullanabiliriz.
#endregion

#region DatabaseGeneratedOption.Identity - ValueGeneratedAdd

//Herhangi bir kolona Identity özelliğini vermemizi sağlayan bir konfigürasyondur.

#region Sayısal Türlerde
//Eğer ki Identity özelliği bir tabloda sayısal olan bir kolonda kullanılacaksa
//o durumda ilgili tablodaki pk olan kolondan özellikle/iradeli bir şekilde identity özelliğinin
//kaldırılması gerekmektedir.
#endregion

#region Sayısal Olmayan Türlerde

//Değer ki identtiy özelliği bir tabloda sayısal olmayan bir kolonda kullanılacaksa o durumda ilgili tablodaki pk olan
//kolondan iradeli bir şekilde identity özelliğini kaldırmaya gerek yoktur.
#endregion

#endregion

#region DatabaseGeneratedOption.Computeed - ValueGeneratedOnAddOrUpdate
//EF Core üzerinde bir kolon computed column ise ister Computed olarak belirleyebilirsiniz
//isterseniz de belirlemeden kullanmaya devam edebilirsiniz
#endregion

#endregion

#endregion


public class Person
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname{ get; set; }
    public int Premium { get; set; }
    public int Salary { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int TotalGain { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PersonCode { get; set; }
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>().Property(p => p.Salary).HasDefaultValue(100);
        //modelBuilder.Entity<Person>().Property(p => p.Salary).HasDefaultValueSql("Select FLOOR(RAND()*1000 )");
        //modelBuilder.Entity<Person>().Property(p => p.TotalGain).HasComputedColumnSql("[Salary]+[Premium]*10").ValueGeneratedOnAddOrUpdate();
        //modelBuilder.Entity<Person>().Property(p => p.PersonId).ValueGeneratedNever();
        //modelBuilder.Entity<Person>().Property(p => p.PersonCode).HasDefaultValueSql("NEW_ID()");
        //modelBuilder.Entity<Person>().Property(p => p.PersonCode).ValueGeneratedOnAdd();


    }
}  