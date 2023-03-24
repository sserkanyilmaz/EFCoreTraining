
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
#region Veri Nasıl Güncellenir?
//ETicaretDbContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(x => x.Id==3 );
//Console.WriteLine(urun.UrunAdi);
//urun.UrunAdi = "H ürünü";
//urun.Fiyat = 999;
//await context.SaveChangesAsync();
#endregion
#region ChangeTracker Nedir? Kısaca!
/*ChangeTracker Context üzerinden gelen verilerin takibinden sorumlu mekanizmadır.
 *Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının 
 *oluşturulacağı anlaşılır.*/
#endregion 
#region Takip Edilmeyen Nesneler Nasıl Güncellenir?
//ETicaretDbContext context = new();
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni ürün",
//    Fiyat = 123
//};
//context.Urunler.Update(urun);// ChangeTracker tarafından takip edilmeyen nesnelerin cünvellenebilmesi için update fonksiyonu kullanılır!!
////Update fonksiyonu kullanabilmek iin kesinlikle ilgili nesnede Id değeri verilmelidir! Bu değer güncellenecek (Update sorgusu oluşturulacak verinin)
////hangisi olduğunu ifade edecektir.
//await context.SaveChangesAsync();

#endregion
#region EntityState Nedir?
//Bir enetiy instance'ının durumunu ifade eden referanstır.
//ETicaretDbContext context = new();
//Urun u = new();
//Console.WriteLine(context.Entry(u).State);

#endregion
#region EF Core Açısından Bir Verinin Güncellenmesi Gerektiği Nasıl Anlaşılıyor?
//ETicaretDbContext context = new();

//Urun urun = await context.Urunler.FirstOrDefaultAsync(x => x.Id == 3);

//Console.WriteLine(context.Entry(urun).State);

//urun.UrunAdi = "Hilmi";

//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(urun).State);
#endregion
#region Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?
ETicaretDbContext context = new();
var urunler = await context.Urunler.ToListAsync();
foreach (var urun in urunler)
{
    urun.UrunAdi += "*";
}
await context.SaveChangesAsync();

#endregion

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