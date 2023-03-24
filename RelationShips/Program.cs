

using Microsoft.EntityFrameworkCore;
#region RelationShips(İlişkiler) Terimleri
#region Principal Entity(Asıl Entity)
//Kendi başına/tek başına/bağımsız bir şekilde var olabilen tabloyu modelleyen entity'e denir. Yani herhangi bir tabloya bağımlılığı olmayan tablodur.

//Departmanlar tablosunu modelleyen 'Departman' entity'sidir.
#endregion

#region Dependent Entity(Bağımlı Entity)
//Kendi başına var olamayan bir başka tabloaya bağımlı(ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.

//Çalışanlar tablosunu modelleyen 'Calisan' entity'sidir.
#endregion

#region Foreign Key
//Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key'dir.

//Entity'ler arasında bu ilişkiyi kurabilmek için kullanacağımız bir tane property var İşte bu property'e foreign key property'si diyeceğiz.

//Dependent Entity'de tanımlanır.

//Principal Entity'deki Principal Key'i tutar.
#endregion

#region Principal Key
//Principal Entity'deki Id'nin ta kendisidir.

//Principal Entity'nin kimliği olan kolonu ifade eder.
#endregion

#endregion

#region Navigation Property Nedir?
//İlişkili tablolar arasındaki fiziksel erişimi entity class'ları üzerinden sağlayan property'lerdir.

//Bir property'nin Navigation Property olabilmesi için kesinlikle entity türünden olması gerekiyor.

//Navigation property'ler entity'lerideki tanımlarına göre n'e n veyahut 1'e n şeklinde ilişki türlerini ifade etmektedirler.
//Sonraki derslerimizde ilişkisel yapıları tam teferruatlı pratikte incelerken navigation property'lerin bu özelliklerden istifade ettiğimizi göreceksiniz.


#endregion
#region İlişki Türleri
#region One to One
//Çalışan ile adresi arasındaki ilişki,
//Karı koca arasındaki ilişki.
#endregion

#region One to Many
//Departman ile çalışan arasındaki ilişki,
//Anne ve çocukları arasındaki ilişki.
#endregion

#region Many to Many
//Çalışanlar ile projeler arasındaki ilişki
//Kardeşler arasındaki ilişki.
#endregion
#endregion

#region Entity Framework Core'da İlişki Yapılandırma Yöntemleri

#region Default Conventions
//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
//Navigation propertyleri kullnarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region Data Annotations Attributes

//Entitylerin niteliklerine göre ince ayarlar yapmamızı sağlayan attribute'lardır.
// [Key] [ForeignKey]
#endregion

#region Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı sağlayan yöntemlerdir.
#region HasOne
//İlgili entitynin ilişkilsel entity'ye birebir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metotdur.

#endregion
#region HasMany
//İlgili entitynin ilişkisel entitye çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.
#endregion
#region WithOne
//HasOne ya da HasMany'den sonra bire bir ya da çoka bir olacak şekilde ilişki yaplandırmasını tamamlayan metottur.
#endregion
#region WithMany
//HasOne ya da HasMany'den sonra bire çok ya da çoka çok olacak şekilde ilişki yaplandırmasını tamamlayan metottur.
#endregion
#endregion

#endregion
Console.WriteLine("asdsadasdasdasd");
public class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ESirketDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

}
public class Calisan
{
    public int Id { get; set; }

    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }
    public Departman Departman { get; set; }
}
public class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }
}