using Microsoft.EntityFrameworkCore;

ETicaretDbContext context = new();
#region AsNoTracking Metodu 
//Context üzerinden gelen tüm datalar ChangeTracker mekanizması tarafından takip edilmektirdir.

//ChangeTracker takip ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde bir maliyete sahiptir.
//O yüzden üzerinde işlem yapılmayacak verilerin takip edilmesi bizlere lüzumsuz yere bir maliyet ortaya çıkaracaktır.

//AsNoTracking metodu, context üzerinden sorgu neticesinde gelecek olan verilerin ChangeTracker tarafından takip edilmesini engeller.

//AsNoTracking metodu ile ChangeTracker'ın ihtiyaç olmayan verilerdeki maliyetini törpülemiş oluruz.

//AsNoTracking fonksiyonu ile yapılan sorgulamalarda, verileri elde edebilir, bu verileri istenilen noktalarda kullanılabilir lakin
//veriler üzerinde herhangi bir değişiklik/update işlemi yapamayız.

//var kullanicilar = await context.Kullanicilar.AsNoTracking().ToListAsync();
//foreach (var kullanici in kullanicilar)
//{
//    Console.WriteLine(kullanici.Adi);
//    kullanici.Adi = $"yeni-{kullanici.Adi}";
//}
//await context.SaveChangesAsync();
#endregion

#region AsNoTrackingWithIdentityResolution

//ChangeTracker mekanizması sayesinde yinelenen datalar ayni instancelari kullanirlar

//AsNoTrackingWithIdentityResolution mekanizması ile yinelenen datalar farkli instancelari kullanirlar

//ChangeTracker mekanizması yenilenen verileri tekil instance olara getirir. Buradan ekstradan bir
//performans kazancı söz konusudur.

//Bizler yaptığımız sorgularda takip mekanızmasının AsNoTracking metodu ile maliyetinin kırmak isterken 
//bazen maliteye sebebiyet vereibliriz (Özellikle ilişkisel tabloları sorgularken bu duruma dikkat etmemiz gerekiyor.)

//AsNoTracking ile elde edilen veriler takip edilmeyeceğinden dolayı yinelenen verilerin ayrı
//instancelarda olmasına sebebiyet veriyoruz. Çünkü ChangeTracker mekanizması takip ettigi nesneden bellekte varsa eger aynı
//nesneden birdaha oluşturma gereği duymaksızın o nesneye ayrı noktalardaki ihtiyacı aynı instance üzerinden gidermektedir.

//Böyle bir durumda hem takip mekanızmasının maliyetini ortadan kaldırmak hem de yinelenen dataları tek bir data üzerinde karşılamak
//için AsNotTrackingWithIdentityResolution fonksiyonunu kullanabiliriz.

//var kitaplar = await context.Kitaplar.Include(k=>k.Yazar).AsNoTracking().ToListAsync();

//AsnoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna nazaran görece yavaştır/maliyetlidir lakin CT'a
//nazaran daha performanslı ve az maliyetlidir.

#endregion

#region AsTracking
//var kitaplar = await context.Kullanicilar.Include(k => k.Roller).AsNoTracking().ToListAsync();
//Kullanicilar track edilmediğinden dolayı her kullanıcının rolü kendi için 1 tane daha instance oluşturulur böylece oluşturulan nesne sayısı kullanıcı *2 olur 

//var kitaplar2 = await context.Kullanicilar.Include(k => k.Roller).AsTracking().ToListAsync();
//kullanicilar track edildiği vakitte ise ortak rollerin 1 kere instanceı oluşturalacağından Astracking AsNoTracking'e nazaran daha az maliyetli hale gelebilir

//Context üzerinden gelen dataların Change Tracker tarafından takip edilmesinin iradeli bir şekilde ifade etmemizi sağlayan fonksiyondur.

//Kullanma sebibimiz => Bir sonraki inceleyeceğimiz UseQueryTrackingBehavior metodunun davranışı gereği uygulama seviyesinde
//Change Tracker'ın default olarak devrede olup olmamasını ayarlıyor olacağız. Eğer ki default olarak pasif hale getirilirse böyle durumlarda takip mekanizmasının ihtiyaç olduğu sorgularda AsTracking 
//fonksiyonunu kullanavilir ve böylece takip mekanizmasını iradeli bir şekilde devreye sokmuş oluruz

//var kitaplar = await context.Kitaplar.AsTracking().ToListAsync();
#endregion

#region UseQueryTrackingBehavior  
//Ef Core seviyesinde/uygulama seviyesinde ilgili contextten gelen verilerin üzreinde
//Change Tracker mekanizmasının davranışı temel seviyede belirlemeizi saplayan fonksiyonudur.
//Yani konfigurasyon fonksiyonudur.

#endregion


Console.WriteLine(  );

public class ETicaretDbContext : DbContext
{
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Rol> Roller { get; set; }
    public DbSet<Kitaplar> Kitaplar { get; set; }
    public DbSet<Yazarlar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB;Database=ETicaretDb");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

}
public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int RolId { get; set; }
    public Rol Roller { get; set; }
}
public class Rol
{
    public int Id { get; set; }
    public string Adi { get; set; }

}
public class Kitaplar
{
    public Kitaplar()
    {
        Console.WriteLine("Kitap nesnesi oluşturuldu");
    }
    public int Id { get; set; }
    public string Adi { get; set; }
    public int YazarId { get; set; }
    public Yazarlar Yazar { get; set; }

}
public class Yazarlar
{
    public Yazarlar()
    {
        Console.WriteLine("Yazar nesnesi oluşturuldu");
    }
    public int Id { get; set; }
    public string Adi { get; set; }
}