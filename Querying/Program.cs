using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

ETicaretDbContext context = new();

#region En Temel Basit Bir Sorgulama Nasıl Yapılır?
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
//linq methodları
#endregion
#region Query Syntax
//var urunler2 = await (from urun in context.Urunler
//                      select urun).ToListAsync();
//linq queryleri
#endregion

#endregion
#region Sorguyu Execute Etmek İçin Ne Yapmanız Gerekmektedir
#region ToListAsync
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler select urun).ToListAsync() ;
#endregion
#endregion
//int urunId = 5;
//string urunAdi = "2";
//var urunler = from urun in context.Urunler
//              where urun.Id >urunId && urun.UrunAdi.Contains(urunAdi)   
//              select urun;
//urunId = 200;
//urunAdi = "4";
//foreach (var urun in urunler)
//  Console.WriteLine(urun.UrunAdi);
//await urunler.ToListAsync();
#region Foreach

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}
#region Deferred Execution(ertelenmiş çalışma)
//IQueryable çalışmalarında ilgili kod yazıldıgı noktada tetiklenmez/çalıştırılmaz yani ilgili kod yazıldığı noktada
//sorguyu generate etmez! Nerede eder? Çalıştırıldugu/execute edildigi noktada tetiklenir! İşte bu duruma ertelenmiş çalışma denir
#endregion
#endregion
#endregion

#region IQueryable ve IEnumerable Nedir ? Basit olarak!
//var urunler =await ( from urun in context.Urunler select urun).ToListAsync();
#region IQueryable
//Sorguya karşılık gelir
//ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.


#endregion
#region IEnumerable 
//Sorgunun çalıştırılıp /execute edilip verilerin memorye yüklemiş haline denir.
#endregion
#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları
#region ToListAsync
//Üretilen sorguyu execute ettirmemizi saglayan fonsiyondur
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion

#region Query Syntax
//var urunler2 = await (from urun in context.Urunler
//               select urunler).ToListAsync();
//var urunler3 =  (from urun in context.Urunler
//                      select urunler);
//var datas = await urunler3.ToListAsync();
#endregion
#endregion
#region Where
//oluşturulan sorguya where şartı eklememizi saglayan bir fonksiyondur
#region Method Syntax
//var urunler = await context.Urunler.Where(x => x.Id > 10).ToListAsync();
//var urunler3 = await context.Urunler.Where(x => x.UrunAdi.StartsWith("a")).ToListAsync();
#endregion

#region Query Syntax
//var urunler2 = from urun in context.Urunler where urun.Id>10 
//               && urun.UrunAdi.EndsWith("2")
//               select urun;
//var datas = await urunler2.ToListAsync();
#endregion
#endregion
#region OrderBy
//sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur
#region Method Syntax
//var urunler = context.Urunler
//    .Where(z => z.Id > 10 || z.UrunAdi.EndsWith("2"))
//    .OrderBy(u=>u.UrunAdi);
#endregion
#region Query Syntax
//var urunler2 = from urun in context.Urunler where urun.Id > 10
//               || urun.UrunAdi.EndsWith("2") 
//               orderby urun.UrunAdi
//               select urun;
//var datas = await urunler2.ToListAsync();
#endregion
#endregion
#region ThenBy
//OrderBy üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamamızı saglayan bir fonksiyondur

//var urunler = await context.Urunler
//    .Where(z => z.Id > 10 || z.UrunAdi.EndsWith("2"))
//    .OrderBy(u => u.UrunAdi)
//    .ThenBy(u=>u.Fiyat)
//    .ThenBy(u=>u.Id).ToListAsync();
#endregion
#region OrderByDescending
//Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur.
#region Method Syntax
//var urunler2 = await context.Urunler.OrderByDescending(u => u.UrunAdi).ToListAsync();

#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              orderby urun.Fiyat descending
//              select urun;
//var datas = await urunler.ToListAsync();
#endregion
#endregion
#region ThenByDescending
#region Method Syntax
//OrderBy descending üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamamızı sağlayan bir fonksiyondur
//var urunler = await context.Urunler
//    .OrderByDescending(x => x.Id)
//    .ThenByDescending(z => z.Fiyat)
//    .ThenBy(z => z.UrunAdi)
//    .ToListAsync();
#endregion

#endregion
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa
//single ya da singleordefault fonksiyonları kullanılabilir
#region SingleAsync
// eger ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır
#region Tek Kayıt Geldiginde
//var urun = await context.Urunler.SingleAsync(x => x.Id == 10);
//Console.WriteLine();
#endregion

#region Hiç Kayıt Gelmediginde

//var urun = await context.Urunler.SingleAsync(x => x.Id == 10000);
//Console.WriteLine();
#endregion

#region Çok Kayıt Geldiğinde

//var urun = await context.Urunler.SingleAsync(x => x.Id >10);
//Console.WriteLine();
#endregion
#endregion


#region SingleOrDefaultAsync
//eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyorsa null döner

#region Tek Kayıt Geldiginde
//var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Id == 10);
//Console.WriteLine();
#endregion

#region Hiç Kayıt Gelmediginde

//var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Id == 10000);
//Console.WriteLine();
#endregion

#region Çok Kayıt Geldiğinde

//var urun = await context.Urunler.SingleOrDefaultAsync(x => x.Id > 10);
//Console.WriteLine();
#endregion
#endregion

//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir

#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir.
//Eğer ki hiç veri gelmiyorsa hata fırlatır
#region Tek Kayıt Geldiginde
//var urun = context.Urunler.First(u => u.Id==55);
#endregion
#region Hiç Kayıt Geldiginde

//var urun = context.Urunler.First(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiginde

//var urun = context.Urunler.First(u => u.Id >55);
#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir.
//Eğer ki hiç veri gelmiyorsa default olarak null değerini döndürür
#region Tek Kayıt Geldiginde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
#endregion
#region Hiç Kayıt Geldiginde

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiginde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id >55);

#endregion
#endregion

#region  SingleAsync , SingleOrDefaultAsync , FirstOrDefaultAsync Fonksiyonlarının Karşılaştırılması

#region Tek Kayıt Geldiginde

#endregion
#region Hiç Kayıt Geldiginde

#endregion
#region Çok Kayıt Geldiginde

#endregion
#endregion

#region FindAsync
//Find fonksiyonu, Primary key kolonuna özel hızlı bir şekilde yapmamızı sağlayan bir fonksiyondur.
//Önce belleğe bakıyor. kaydı bulamazsa veritabanına sorgu gönderiyor
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);

//var urun = await context.Urunler.FindAsync(55);
#region Composite Primary Key Durumu
//UrunParca urunParca = await context.UrunParca.FindAsync(2,5);
#endregion
#endregion
#region FindAsync ile SingleAsync , SingleOrDefaultAsync , FirstOrDefaultAsync Fonksiyonlarının Karşılaştırılması

#endregion


#region LastAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. OrderBy kullanılması mecburidir.
//Eğer ki hiç veri gelmiyorsa hata fırlatır
//var urunler = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u => u.Id > 50);
#endregion
#region LastOrDefaultAsync
//Sorgu neticesinde gelen verilerden en sonuncusunu getirir. OrderBy kullanılması mecburidir.
//eğer ki hiç veri gelmiyorsa null döner.
//var urunler = await context.Urunler.OrderBy(u => u.Fiyat).LastOrDefaultAsync(u => u.Id > 50);

#endregion

#region LastAsync ile SingleAsync , SingleOrDefaultAsync , FirstOrDefaultAsync Fonksiyonlarının Karşılaştırılması


#region FindAsyns ile Single
#region Method Syntax

#endregion
#region Query Syntax

#endregion
#endregion
#endregion

#endregion

#region Diğer Sorgulama Fonksiyonları

#region CountAsync
//oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak bzilere bildiren fonksiyondur.
//var urunler = (await context.Urunler.ToListAsync()).Count();
//var urunler = await context.Urunler.CountAsync();
//Console.WriteLine(urunler);

#endregion

#region LongCountAsync
//int deger aralıgını aşarsa veri sayısı bu fonksiyonu kullanırız
//var urunler = await context.Urunler.LongCountAsync(u=>u.Fiyat  ==50);
//Console.WriteLine(urunler);
#endregion

#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur.
//var urunler = await context.Urunler.Where(x=>x.UrunAdi.Contains("1")).AnyAsync();
//var urunler2 = await context.Urunler.AnyAsync(x => x.UrunAdi.Contains("1"));

#endregion

#region MaxAsync
//verilen kolondaki max değeri getirir
//var fiyat = await context.Urunler.MaxAsync(x => x.Fiyat);
#endregion

#region MinAsync
//verilen kolondaki min değeri getirir
//var fiyat = await context.Urunler.MinAsync(x=>x.Fiyat);
#endregion

#region Distinc

//Sorguda mükerrer/tekrar eden kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
//var urunler = await context.Urunler.Distinct().ToListAsync();
//distinc fonksiyonunu execute etmemiz gerekir.
#endregion

#region AllAsync
//Bir sorgu neticesinde gelen verilerin, verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true
// uymuyorsa false döndürecektir
//var m = await context.Urunler.AllAsync(x=>x.Fiyat >=20);
//var m = await context.Urunler.AllAsync(x=>x.UrunAdi.Contains("a"));
#endregion

#region SumAsync
//vermiş oldugumuz sayısal propertynin toplamını alır.
//var fiyat = await context.Urunler.SumAsync(x=>x.Id);
#endregion

#region AverageAsync
//vermiş oldugumuz sayısal propertynin aritmetik ortalamasını alır.
//var ort = await context.Urunler.AverageAsync(x => x.Id);
#endregion

#region ContainsAsync
//Like '%...%'sorgusu oluşturmamızı sağlar
//var urunler = await context.Urunler.Where(x => x.UrunAdi.Contains("7")).ToListAsync(); ;
#endregion
#region StartWith
//Like '%...%'sorgusu oluşturmamızı sağlar
//var urunler = await context.Urunler.Where(x => x.UrunAdi.StartsWith("7")).ToListAsync(); ;
#endregion
#region EndssWith
//Like '%...%'sorgusu oluşturmamızı sağlar
//var urunler = await context.Urunler.Where(x => x.UrunAdi.EndsWith("7")).ToListAsync(); ;
#endregion
#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları 

// bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultusunda farklı türlerde projecsion edebiliyoruz

#region ToDictionaryAsync

//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılamak istiyorsak kullanılır

//  var urunler  = await context.Urunler.ToDictionaryAsync(x=>x.Id, x=>x.Fiyat);

//ToList ile aynı amaca hizmet etmektedir . Yani , oluşturulan sorguyu execute nedip neticesinin alırlar.
//ToList : Gelen sorgu neticesinin entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken
//ToDictionary ise : gelen sorgu neticesinde Dictionart türünden bir koleksiyona dönüştürmektedir.

#endregion

#region ToArrayAsync

//Oluşturulan sorguyu dizi olarak elde eder.
//ToList ile muadil amaca hizmet eder . Yani sorguyu execute eder lakin sonucu entity dizisi olarak elde eder

//var urunler = await context.Urunler.ToArrayAsync();

#endregion

#region Select
//select fonksiyonu işlevsel olarak birden fazla davranışı söz konusudur.
//select fonksiyonu generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır.
//var urunler = await context.Urunler.Select(x=> new Urun
//{
//    Id=x.Id,
//    Fiyat=x.Fiyat,
//}).ToListAsync();

//2. Select fonksiyonu gelen verileri farklı türlerde karşılamamızı sağlar. T, anonim
//var urunler = await context.Urunler.Select(x => new
//{
//    Id = x.Id,
//    Fiyat = x.Fiyat,
//}).ToListAsync();

//var urunler = await context.Urunler.Select(x => new UrunDetay
//{
//    Id = x.Id,
//    Fiyat = x.Fiyat,
//}).ToListAsync();
#endregion

#region SelectMany
//Select ile aynı amaca hizmet eder lakin ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar
//var urunler = await context.Urunler.Include(u=>u.Parcalar).SelectMany(u=> u.Parcalar, (u,p) =>new 
//{
//    u.Id,
//    u.Fiyat,
//    p.ParcaAdi

//}).ToListAsync();

#endregion
#endregion

#region GroupBy Fonksiyonu
//gruplama yapmamızı sağlayan fonksiyondur.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//   Fiyat = group.Key
//}).ToListAsync();
#endregion
#region Query Syntax
//var datas = await (from urun in context.Urunler
//            group urun by urun.Fiyat
//            into @group
//            select new
//            {
//                Fiyat = @group.Key,
//                Count= @group.Count(),
//            }).ToListAsync();
//foreach (var data in datas)
//    Console.WriteLine(data);
#endregion
#endregion

#region Foreach Fonksiyonu
//bir sorgulama fonksiyonu değildir!
//sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak
//dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan fonksiyonudur.
//foreach döngüsünün metot halidir.
//var datas = await (from urun in context.Urunler
//                   group urun by urun.Fiyat
//            into @group
//                   select new
//                   {
//                       Fiyat = @group.Key,
//                       Count = @group.Count(),
//                   }).ToListAsync();
//datas.ForEach(data =>
//{
//    Console.WriteLine(data);
//});
#endregion

Console.WriteLine();
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
        modelBuilder.Entity<UrunParca>().HasKey(x => new {x.UrunId,x.ParcaId });
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