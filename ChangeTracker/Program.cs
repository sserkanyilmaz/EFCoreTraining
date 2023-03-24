// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
ETicaretDbContext context = new ETicaretDbContext();
#region ChangeTracker Neydi?
//Context nesnesi üzerinden gelen tüm nesneler/veriler otomatik olarak bir takip mekanizmasi tarafından izlenirler
//İşte bu takip mekanızmasına ChangeTracker denir. ChangeTracker ile nesneler üzerindeki değişiklikler/işlemler
//takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql sorgucukları genetare edilir.
//işte bu işleme de Change Tracking denir.
#endregion
#region ChangeTracker Propertysi
//Takip edilen nesnelere erişebilmemizi sağlayan ve
//gerektiği takdirde işlemler gerçekleşmesini saplayan bir properydir.

//Context sınıfının base class'ı olan DbContext sınıfının bir member'ıdır.

//var urunler = await context.Urunler.ToListAsync();

//gelen bütün veriler ChangeTracker tarafından izlenmeye başladı
//urunler[6].Fiyat = 123;
//context.Urunler.Remove(urunler[7]);
//urunler[8].UrunAdi = "urunadi";
//var datas = context.ChangeTracker.Entries();
//await context.SaveChangesAsync();

#endregion
#region DetechChanges Metodu
/*
EF Core context nesnesi tarafından izlenen tüm nesnelerdeki değişiklikleri Change Tracher
Sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlaık görüntülerini
oluşturabilir
Yapılan değişikliklerin veritabanına gönderilmeden önce algılandıgından emin olmak gerekir. SaveChanges
fonksiyonu çağrıldığı anda nesnelere EF Core tarafında otomatik kontrol edilirler.
Ancak yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişikliklerin algılanmasının
opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar
Ef Core değişiklikleri otomatik olarak algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.
*/

//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//urun.Fiyat = 123;

//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();

#endregion
#region AutoDetectChangesEnabled Propery'si
/*
İlgili metotlar(SaveChanges, Entries)  tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigurasyonunu yapmamızı sağlayan propertydir
SaveChanges fonksiyonu tetikleindiğinde DetectChanges metodunu içierisinde default olarak çağırmaktadır.
Bu surumda DetectChanges fonksiyonunun kullanımını irademizler yönetmek ve maliyet/performans optimizasyonu yapmak iştediğimiz 
durumlarda AutoDetectChangeEnabled özelliğini kapatabiliriz.
 */
#endregion

#region Entries Metodu
/*
Context'te ki entry metodunun koleksiyonel versiyonudur.
ChangeTracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry türünden elde etmemizi saplar ve belirli işlemler yapabilmemize olanak tanır.
Entries metodu, DetectChanges metodunu tetikler. Bu durumda tıpkı SaveChanges'da olduğu gibi bir maliyettir.
Buradaki maliyette nkaçınmak için AutoDetectChangesEnabled özelliğine false değeri verilebilir
*/
//var urunler = await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u=>u.Id==7).Fiyat = 123;
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 7));
//urunler.FirstOrDefault(u => u.Id == 7).UrunAdi = "urunadi";

//context.ChangeTracker.Entries().ToList().ForEach(entry =>
//{
//    if (entry.State == EntityState.Unchanged)
//    {
//        //...
//    }
//    else if (entry.State == EntityState.Deleted)
//    {
//        //...
//    }
//    //...
//});
#endregion

#region AcceptAllChanges Metodu
/*
SaveChanges() veya SaveChanges(true) Ef Core her şeyin yolunda oldupunu varsayarak track ettiği verilerin takibini
keser yani yeni teğişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum/olası bir hata söz konusu
olursa eğer Ef Core Takip ettiği nesneleri bırakacağı için bir düzeltme mevzu bahis olamayacaktır.
 
Haliyle bu durumda SaveChanges(false) ve AcceptAllChanges metotları girecektir.
SaveChanges(false) başarısızlık durumunda dahi takip etmeyi bırakmıyor.
eğer SaveChanges(false) yapıp başarılı olduğuna eminsek AcceptAllChanges ile takiplerin bırakılmasını sağlıyor.

SaveChanges(false) EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde
yeniden oynatılabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye devam eder. Taa ki AcceptAllChanges metodunu
irademizle çağırana kadar.

SaveChanges(false) ile işlemin başarılı olduğuna emin olursanız AcceptAllChanges ile nesnelerin takibini kesebiliriz.

*/

//var urunler = await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123;
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 7));
//urunler.FirstOrDefault(u => u.Id == 7).UrunAdi = "urunadi";

//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();
#endregion

#region HasChangesMetodu
//takip edilen nesneler arasında değişiklik olup olmadığını kontrol eder.
//Takip edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.
//var result = context.ChangeTracker.HasChanges();
#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder
#region Detached
//Nesnenin ChangeTracker takip edilmediğini ifade eder
//Urun urun = new ();
//Console.WriteLine(context.Entry(urun).State);
#endregion
#region Added
//Veritabanına eklenecek nesneyi ifade eder. Added henüz eritabanına işlenmeyen veriyi ifade eder.
//SaveChanges fonksiyonu çağrıldığında insert sorgusu oluşturulacağı anlamına gelir.


//Urun urun = new Urun();
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "asdfsfas";
//await context.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//urun.Fiyat = 1234;
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);

#endregion
#region Unchanged
//Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder.
//Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir
//var urunler = await context.Urunler.ToListAsync();
//var data = context.ChangeTracker.Entries();
//Console.WriteLine();
#endregion

#region Modified
//Nesne üzerinde değişiklik yani güncelleme yapıldığını ifade eder. SaveChanges çağrıldığında update sorgusu oluşacağı anlamına gelir.


//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "sdasd";
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);

#endregion
#region Deleted
//Nesneinin  silindiğini ifade eder. SaveChanges fonksiyonu çağırıldığında delete sorgusu çalıştırılacağı anlamına gelir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);
//context.Remove(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();

#endregion

#endregion

#region Context Nesnesi üzerinden ChangeTracker

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
//urun.Fiyat = 1234;//Modified ||  Update
//urun.UrunAdi = "armut";
#region Entry Metodu

#region OriginalValues Property'si ***************************************************
//var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(urun.Fiyat));
//var urunAdi = context.Entry(urun).OriginalValues.GetValue<string>(nameof(urun.UrunAdi));
#endregion

#region CurrentValues Property'si
//var fiyat = context.Entry(urun).CurrentValues.GetValue<float>(nameof(urun.Fiyat));
//var urunAdi = context.Entry(urun).CurrentValues.GetValue<string>(nameof(urun.UrunAdi));
#endregion
#region GetDatabaseValues Metodu
//var _urun = await context.Entry(urun).GetDatabaseValuesAsync();
#endregion
#endregion
#endregion

#region Change Tracker'ın Interceptor olarak kullanılması



#endregion

Console.WriteLine(  );











public class ETicaretDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB;Database=ETicaretDb");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(x => new { x.UrunId, x.ParcaId });
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if(entry.State==EntityState.Added)
            {
                //...
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }

}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
    public float Fiyat { get; set; }

}
public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}
public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }

}