using Microsoft.EntityFrameworkCore;
Console.WriteLine();
public class ETicaretContext : DbContext
{
    DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider 
        optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=test;User=SA;Password=reallyStrongPwd123;;TrustServerCertificate=true");


        //Connection String
        //Lazy Loading 
        //vb..
    }
}
public class Urun
{
    public int Id { get; set; }/*
    public int id { get; set; }
    public int UrunID { get; set; } olarak tanımladıgımız her prop primary key olarak algılanır
    public int UrunID { get; set; }*/

}
#region OnConfiguring  ile Konfügrasyon Ayarlarını Gerçekleştirmek
//EF Core  tool'unu yapılandırmak için kullandığımız bir metottur.
//Context nesnesinde override edilerek kullanılmaktadır
#endregion
#region Basit düzeyde entity tanımlama kuralları
//EF Corei her tablonun default olarak bir primart key dolonu olması gerektiğini kabul eder
//Haliyle bu kolonu temsil eden bir property tanımlamadığımız takdirde hata verecektir

#endregion
#region Tablo Adını Belirleme

#endregion