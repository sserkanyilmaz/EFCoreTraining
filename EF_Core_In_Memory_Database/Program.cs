


using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();

//Migration oluşturmaya gerek yok.
//In-Memory'de oluşturulmuş olan database uygulama sonra erdiği/kapatıldığı takdirde silinecektir.
//dolayısıyla özellikle gerçek uygulamalarda in-memory database'i kullanıyorsanız bunun kalıcı değil geçiçi oldugunu unutmamalıyız.

#region EF Core'da In-Memory Database Ila Çalışmanın Gereği Nedir?
//EF Core fiziksel veritabanlarından ziyade in-memory'de database oluşturup birçok işlemi yapmamızı sağlayabilmektedir. işte bu özellik ile gerçek uygulamaların dışında test gibi operasyonları hızlıca yürütebileceğimiz imkanlar sağlar.
#endregion
#region Avantajları
//Test ve pre-prod uygulamalarda gerçek yani fizikksel veritabanları oluşturmak ve yapılandırmak yerine tüm veritabanını bellekte modelleyebilir ve gerekli işlemleri
//sanki gerçek bir veritabanında çalışıyor gibi orada gerçekleştirebiliriz.

//Bellekte çalışmak geçici bir deneiym olacağı için serverlarında test amaçlı üretilmiş olan veritabanlarının lüzumsuz yer işgal etmesini engellemiş olacaktır.

//Bellekte veritabanını modellemek kodun hızlı bir şekilde test edilmesini sağlayacaktır.
#endregion

#region Dezavantajları Nelerdir?

//In-memory'de yapılacak olan veritabanı işlevlerinde ilişkisel modellemeler yapılamamaktadır. bu durumdan dolayı veri tutarlılığı sekteye uğrayabilir ve istatiksel açıdan yanlış sonuçlar elde edilebilir.

#endregion
#region Örnek Çalışma
await context.Persons.AddAsync(new() { Name = "serkan", Surname = "Yılmaz" });
await context.SaveChangesAsync();

var persons = await context.Persons.ToListAsync();

#endregion
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("exampleDatabase");
    }

}