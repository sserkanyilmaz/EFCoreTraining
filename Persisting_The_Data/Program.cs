using Microsoft.EntityFrameworkCore;

#region Veri Nasıl Eklenir
/*ETicaretDbContext context = new ETicaretDbContext();
Urun urun = new()
{
    UrunAdi = "A urunu",
    Fiyat = 1000
};*/
/*
#region Context.AddAsync fonksiyonu
//await context.AddAsync(urun); // tur korumasız. Entity belirtmedigimiz için object türünde alıyor
#endregion
#region Context.dbset.AddAsync fonksiyonu
await context.Urunler.AddAsync(urun); // tur korumali. Entity belirttigimiz için belirttigimiz entitynin türünde alıyor.
#endregion
await context.SaveChangesAsync();*/
#endregion
#region SaveChanges nedir?
/*
 * Save Changes insert, update ve delete fonksiyonlarını oluşturup bir transaction eşliğinde veritabanına gönderip
 * execute eden fonksiyondur. Eğer ki oluşturulan sorgulardan herhangi biri başarısız olursa tüm işlemleri geri alır.
 * (rollback)
 */

#endregion
#region EF Core Açısından Bir Verinin Eklenmesi Gerektiği Nasıl Anlaşılıyor
/*ETicaretDbContext context = new();
Urun urun = new Urun()
{
    UrunAdi = "B urunu",
    Fiyat = 2000
};
Console.WriteLine(context.Entry(urun).State);//detached 
await context.AddAsync(urun);
Console.WriteLine(context.Entry(urun).State);//added
await context.SaveChangesAsync();
Console.WriteLine(context.Entry(urun).State);//Unchanged*/
#endregion

#region Birden Fazla Veri Eklerken Nelere Dikkat Edilmelidir
/*ETicaretDbContext context = new();
Urun urun1 = new Urun()
{
    UrunAdi = "C urunu",
    Fiyat = 2000
};
Urun urun2 = new Urun()
{
    UrunAdi = "D urunu",
    Fiyat = 2000
};
Urun urun3 = new Urun()
{
    UrunAdi = "E urunu",
    Fiyat = 2000
};
await context.AddAsync(urun1);

await context.AddAsync(urun2);
 await context.AddAsync(urun3);

await context.SaveChangesAsync();*/

#region SaveChanges'ı Verimli Kullanalım !!
//SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolayı 
//Ef Core ile yapılan her bi işleme özel kullanmaktan kaçınmalıyız.
//çünkü her işleme özel transaction veritabanı açısından ekstradan maliyet demektir.
//O yüzden mümkün mertebe tüm işlemlerimi tek bir transaction eşliğinde veritabanına 
//gönderebilmek için savechange'ı aşağıdaki gibi tek seferde kullanmak hem malitey hem de 
//yönetilebilirlik açışından katkıda bulunmuş olacaktır.
//ETicaretDbContext context = new();
//Urun urun1 = new Urun()
//{
//    UrunAdi = "C urunu",
//    Fiyat = 2000
//};
//Urun urun2 = new Urun()
//{
//    UrunAdi = "D urunu",
//    Fiyat = 2000
//};
//Urun urun3 = new Urun()
//{
//    UrunAdi = "E urunu",
//    Fiyat = 2000
//};
//await context.AddAsync(urun1);

//await context.AddAsync(urun2);

//await context.AddAsync(urun3);

//await context.SaveChangesAsync();
#endregion
#region AddRange
//ETicaretDbContext context = new();
//Urun urun1 = new Urun()
//{
//    UrunAdi = "C urunu",
//    Fiyat = 2000
//};
//Urun urun2 = new Urun()
//{
//    UrunAdi = "D urunu",
//    Fiyat = 2000
//};
//Urun urun3 = new Urun()
//{
//    UrunAdi = "E urunu",
//    Fiyat = 2000
//};

//await context.Urunler.AddRangeAsync(urun1, urun2, urun3);

//await context.SaveChangesAsync();
#endregion
#endregion
#region Eklenen Verinin Generate Edilen Id'sini Elde Etme
//son eklenen verinin ıd'sini elde etme
ETicaretDbContext context = new();
Urun urun1 = new Urun()
{
    UrunAdi = "O urunu",
    Fiyat = 2000
};
await context.AddAsync(urun1);
await context.SaveChangesAsync();
Console.WriteLine(urun1.Id);
#endregion










Console.WriteLine();
public class ETicaretDbContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB;Database=ETicaretDb");
    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

}