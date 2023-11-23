// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection;

Console.WriteLine("Hello, World!");

#region OnModelCreating
/*
 * Genel anlamda veritabanı ile ilgili konfigürasyonel operasyonların dışında entityler üzerinde konfigürasyonel çalışmalar
 * yapmamızı sağlayan bir fonksiyondur
 */
#endregion

#region IEntityTypeConfiguration<T> Arayüzü
/*
 * Entity bazlı yapılacak olan konfigürasyonları o entitye özel harici bir dosya üzerinde yapmamızı sağlayan bir arayüzdür 
 * 
 * Harici bir dosyada konfigürasyonların yürütülmesi merkezi bir yapılandırma noktası oluşturmamızı sağlamaktadır.
 * 
 * Harici bir dosyada konfigürasyonların yürütülmesi entity sayısının fazla olduğu senaryolarda yönetilebilirliği arttıracak
 * ve yapılandırma ile ilgili geliştiricinin yükünü azaltacaktır.
*/
#endregion


#region ApplyConfiguration Metodu
/*
 * Bu metot harici konfigurasyonel sınıflarımızı EF Core'a bildirek için kullandığımız metottur.
 */
#endregion

#region ApplyConfigurationsFromAssembly Metodu
/*
 * uygulama bazında oluşturulan harici konfigürasyonel sınıfların her birini OnModelCreating metodunda bildirmek yerine
 * ApplyConfiguration ile tek tek bildirmek yerine bu sınıfların bulunduğu Assembly'i bildirerek IEntityTypeConfiguration
 * arayüzünden türeyen tüm sınıfları ilgili entitye karşılık konfigürasyonel değer olarak baz almasını tek kalemde 
 * gerçekleştirmemizi sağlayan bir metottur.
 */
#endregion
















public class Order
{
    public int OrderId { get; set; }
    public string Descrition { get; set; }
    public DateTime OrderDate { get; set; }
}
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o=>o.OrderId);
        builder.Property(o=>o.Descrition).HasMaxLength(50);
        builder.Property(o => o.OrderDate).HasDefaultValueSql("GETDATE()");
    }
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()) ;
    }
}