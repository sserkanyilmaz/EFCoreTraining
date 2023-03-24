using Microsoft.EntityFrameworkCore;
#region Veri Nasıl Silinir?
//ETicaretDbContext context = new ETicaretDbContext();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();
//Console.WriteLine();

#endregion
#region Silme İşleminde ChangeTracker'ın Rolü
//Context üzerinden veri geldiği için changetracker bu veriyi takip eder.
#endregion
#region Takip Edilmeyen Nesneler Nasıl Silinir
//ETicaretDbContext context = new ETicaretDbContext();
//Urun urun = new Urun() { Id = 2};
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();
//Console.WriteLine();
#endregion
#region EntityState İle Silme İşlemi
//ETicaretDbContext context = new ETicaretDbContext();
//Urun urun = new Urun() { Id = 1 };
//context.Entry(urun).State = EntityState.Deleted;
//await   context.SaveChangesAsync();
#endregion
#region Birden Fazla Veri Silerken Nelere Dikkat Edilmelidir?

#endregion
#region SaveChanges'ı Verimli Kullanalım

#endregion
#region RemoveRange
//birden fazla veriyi silerken tek tek silmek yerine tek seferde silmek için kullanılıyor
ETicaretDbContext context = new ETicaretDbContext();
List<Urun> urunler = await context.Urunler.Where(x => x.Id >= 7 && x.Id <= 10).ToListAsync();
context.Urunler.RemoveRange(urunler);
await context.SaveChangesAsync();
Console.WriteLine();
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