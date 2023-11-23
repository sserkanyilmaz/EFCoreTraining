using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

ApplicationDbContext context = new();

#region EF Core'da Neden Yapılandırmalara ihtiyacımız olur?
//Default davranışları yeri geldiğinde geçersiz kılmak ve özelleştirmek isteyebiliriz. 
//Bundan dolayı yapılandırmalara ihtiyacımız olacaktır.

#endregion

#region OnModelCreating Metodu
/*
 * EF Core'da yapılandırma diyince akla ilk gelen metod OnModelCreating metodudur.
 * Bu metot, DbContext sınıfı içerisinde virtual olarak ayalanmış bir metottur.
 * Bizler bu metodu kullanarak model'larımızla ilgili temel konfigurasyonel davranışları(Fluent API) sergileyebiliriz.
 * Bir model'ın yaratılışıyla ilgili tüm konfigurasyonları burada gerçekleştirebilmekteyiz.
*/

#region GetEntityTypes
//EF Core'da kullanılakn entity'leri elde etmek, programatik olarak öğrenmek istiyorsak eüer GetEntityTypes fonksiyonunu kullanabiliriz.


#endregion

#endregion

#region Configuration | Data Annotations & Fluent API

#region Table | ToTable
//Generate edilecek tablonun ismini belirlememizi sağlayan yapılandırmadır. 
//EF Core normal şartlarda generate edeceği tablonun adını DbSet property'sinden almaktadır. Bizler eğer ki bunu özelleştirmek istiyorsak
//Table attribute'unu yahut ToTable api'ını kullanabiliriz.
#endregion

#region Column - HasColumnName, HasColumnType, HasColumnOrder
//EF Core'da tabloların kolonları entity sınıfları içerisindeki property'lere karşılık gelmektedir.
//Default olarak property'lerin adı kolon adıyken  , türleri/tipleri kolon türleridir.
//Eğer ki generate edilecek kolon isimlerine ve türlerine müdahale etmek istiyorsak bu konfigurasyonlar kullanılır.
#endregion

#region ForeignKey -HasForeignKey
/*İlişkisel tablo tasarımlarında bağımlı tabloda esas tabloya karşılık gelecek verilerin
 * tutulduğu kolonu foreign key olarak temsil etmekteyiz
 * 
 * EF Core'da foreign key kolonu genellikle Entity Tanımlama kuralları gereği default yapılanmalarla oluşturulur.
 *
 *ForeignKey Data annotations attribute'unu direkt kullanabilirsiniz. Lakin Fluent api ile bu konfigurasyonu yapacaksanız iki entity arasındaki ilişkiyi de modellemeniz 
 *gerekmektedir. Aksi takdirde fluent api üzerinde HasForeignKey fonksiyonunu kullanamazsınız.
 */
#endregion

#region NotMapped - Ingore
/*
 * Ef Core entity sınıfları içerisindeki tüm propertyleri default olarak modellenen tabloya kolon şeklinde migrate eder.
 * Bazen bizler entity sınıfları içerisinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak mecburiyetinde kalabiliriz.
 * 
 * Bu Propertylerin Ef Core tarafından kolon olarak map edilmesini istemediğimizi bildirmek için NotMapped ya da Ignore kullanabiliriz.
 */
#endregion

#region Key-HasKey
//EF Core'da, default convention olarak bir entity'nin içerisindeki Id,ID ,EntityId, EntityID vs. şeklinde tanımlanan tüm propertyler varsayılan 
//olarak primary key constraint uygulanır.

//Key ya da HasKey yapılandırmalarıyla istediğimiz herhangi bir property'e default convention dışında primarykey özelliği verebiriliz.

//EF Core'da bir entity içerisinde kesinlikle PK'i temsil edecek olan property bulunmalıdır. Aksi takdirde EF Core migration oluştururken hata verecektir
//Eğer ki tablonun PK'i yoksa bunun bildirilmesi gerekir.
#endregion

#region Timestamp- IsRowVersion
//to do timestamp
//ileriki süreçlerde veri tutarlılığı ile ilgilenirken burası doldurulacak.

//Bir satırdaki verinin bütünsel olarak değişikliğini takip etmemizi sağlayacak olan versiyon mantığını sağlar.

//Bir verinin verisyonunu oluşturmamızı sağlayan yapılanmadır.

//[Table("Kisiler")]
//Person person =await context.Persons.FindAsync(1);
//person.Name = "Sserkan";
//await context.SaveChangesAsync();
//person.Department=new Department() { Name="Yazılım departmanı"};
//person.Name = "Serkan";
//await  context.Persons.AddAsync(person);
//await context.SaveChangesAsync();

#endregion

#region Required - IsRequired
//Bir kolonun nullable ya da not null olup olmamasını belirleyebiliriz.

//EF Core'da bir property default olarak not null şeklinde tnaımlanır. Eğer ki property'si nullable yapmak istiyorsak türü üzerinde ?(nullable)
//operatörü ile EF Core'a bildirmemiz gerekmekte.
#endregion

#region MaxLenght |StringLenght - HasMaxLenght
//bir kolonun max karakter sayısını belirlememizi sağlar
#endregion

#region Precision -HasPrecision
//Küsüratlı sayılarda bir kesinlik ve noktanın hanesini bildirmemizi sağlayan bir yapılandırmadır

#endregion


#region Unicode - IsUnicode
//Kolon içerisinde Unicode karakterler kullanılacaksa bu yapılandırılma kullanılabilir.

#endregion

#region Comment - HasComment
//EF Core üzerinden oluşturulmuş olan veritabanı nesneleri üzerinde bir açıklama/yorum yapmak için kullanılır.

#endregion

#region ConcurrencyCheck
//ileriki süreçlerde veri tutarlılığı ile ilgilenirken burası doldurulacak.

//Bir satırdaki verinin bütünsel olarak değişikliğini takip etmemizi sağlayacak concurrency token yapılandırmasını kullanacağız

//Bir verinin verisyonunu oluşturmamızı sağlayan yapılanmadır.
#endregion

#region InverseProperty
//İki entity arasında birden fazla ilişki varsa eğer bu ilişkilerin hangi navigation propertyler üzerinden olacağını ayarlamamızı sağlayan configurasyondur.



#endregion

#endregion

#region Comfigurations |Fluent API

#region Composite Key
//Tablolarda birden fazla kolonu kümülatif olarak primary key yapmak istiyorsak buna composite key denir.

//
#endregion
#region HasDefaultSchema
//EF Core üzerinden inşa edilen herhangi bir veritabanı nesnesi default olarak dbo şemasına sahiptir. Bunu özelleştirebilmek için kullanılan bir yapılandırmadır.

#endregion

#region Property


#region HasDefaultValue
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda hangi değeri alacağını belirler.

//Person person = new() { Name = "Serkan", Surname = "Yılmaz", Department = new() { Name="Department1"}, CreatedDate = DateTime.Now };
//await context.Persons.AddAsync(person);
//await context.SaveChangesAsync();
#endregion
#region HasDefaultValueSql
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi sql cümleciğinden alacagını belirler
#endregion
#endregion

#region HasComputedColumnSql
//Tablolarda birden fazla kolondaki verileri işleyerek değerini oluşturan kolonlara Computed Column denmektedir.
//EF Core üzerinde bu tarz computed column oluşturabilmek için kullanılan bır yapılandırmadır.
#endregion

#region HasConstraintName
//EF Core üzerinde oluştururken constraint'lere default isim yerine özelleştirilmiş bir isim verebilmek için kullanılan yapılandırmadır.
#endregion

#region HasData
//Seed Datayı işlerken veritabanını oluştururken bir yandan da verileri işlemek istiyorsak bunu kullanıyoruz.
//İşte HasData konfigurasyonu bu operasyonun yapılandırma ayağıdır. 
//HasData ile migrate sürecinde oluşturulacak olan verilerin pk olan id kolonlarına iradeli bir şekilde değer girilmesi zorunludur
#endregion

#region HasDiscriminator
//ileride entityler arasdına kalıtımsal ilişkilerin olduğu TPT ve TPH isminde konuları inceliyor olacağız. işte bu konularla ilgili yapılandırmalarımız
//HasDiscriminator ve HasCalue fonksiyonlarıdır.
#endregion
#region HasField
//Backing Field özelliğini kullanmamızı sağlayan bir yapılandırmadır
#endregion

#region HasNoKey
//Normal şartlarda EF Core'da tüm entitylerin bir PK kolonu olmak zorundadır.
//Eğer ki entitylerde pk kolonu olmayacaksa bunu bildirmek için HasNoKey konfigürasyonu kullanılır.
#endregion
#region HasIndex
//Bu yapılandırmaya dair konfigürasyonlarımız HasIndex ve Index attribute'dur.
#endregion

#region HasQueryFilter
//Global Query Filter adı altında sonradan işlenecek.
//Temeldeki görevi bir entitye karşılık uygulama bazında global bir filtre koymaktır.
#endregion

#region DatabaseGenerate - ValueGeneratedOnAddOrUpdate, ValueGeneratedOnAdd, ValueGeneratedNever

#endregion

#endregion
public abstract class Entity
{
    public int Id { get; set; }
    public string X { get; set; }
}
public class A : Entity
{ 
    public int Y { get; set; }
}
public class B : Entity
{ 
    public int Z { get; set; }
}
public class Example
{
    public int x { get; set; }
    public int y { get; set; }
    public int Computed { get; set; }
}

public class Person
{
    //[Key]
    public int Id { get; set; }
    //public int Id2 { get; set; }
    public int DepartmentId { get; set; }
    //[ForeignKey(nameof(Department))]
    //public int DId{ get; set; }
    //[Column("Adi",TypeName ="metin",7)]
    //[MaxLength(100)]
    public string _name { get; set; }
    public string? Name { get=> _name; set=> _name = value; }
    //[Required()]
    //[Unicode]
    public string? Surname { get; set; }
    //[Precision(5,3)]
    public decimal Salary { get; set; }
    //[NotMapped]
    //[Timestamp]
    //[Comment("Versiyon kontrolü")]
    //public Byte[] RowVersion { get; set; }
    //Yazılımsal amaçla oluşturulan prop
    //[ConcurrencyCheck]
    public int ConcurrencyCheck { get; set; }
    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }
}
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Person> Persons { get; set; }
}
public class ApplicationDbContext : DbContext
{
    

    //public DbSet<Entity> Entities{ get; set; }
    //public DbSet<A> As { get; set; }
    //public DbSet<B> Bs { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Department> Departments { get; set; }
    //public DbSet<Flight> Flights { get; set; }
    //public DbSet<Airport> Airports { get; set; }
    public DbSet<Example> Examples { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region GetEntityTypes
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entityType in entities)
        //    Console.WriteLine(entityType);
        #endregion
        #region ToTable
        //modelBuilder.Entity<Person>().ToTable("Kisiler");
        #endregion
        #region Column
        //modelBuilder.Entity<Person>().Property(p => p.Name)
        //.HasColumnName("Adi")
        //.HasColumnType("metin")
        //.HasColumnOrder(7);
        #endregion
        #region ForeignKey
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.DId);
        #endregion
        #region Ignore
        //modelBuilder.Entity<Person>().Ignore(p => p.BrutMaas);
        #endregion
        #region HasKey
        //modelBuilder.Entity<Person>().HasKey(p => p.Id);
        //modelBuilder.Entity<Person>().HasNoKey();
        #endregion
        #region IsRowVersion
        //modelBuilder.Entity<Person>().Property(p => p.RowVersion).IsRowVersion();
        #endregion
        #region IsRequired
        //modelBuilder.Entity<Person>().Property(p => p.Surname).IsRequired();
        #endregion
        #region HasMaxLenght
        //modelBuilder.Entity<Person>().Property(p => p.Name).HasMaxLength(100);
        #endregion
        #region HasPrecision
        //modelBuilder.Entity<Person>().Property(p => p.Salary).HasPrecision(5, 3);
        #endregion
        #region IsUniCode
        //modelBuilder.Entity<Person>().Property(p=>p.Surname).IsUnicode();
        #endregion
        #region HasComment
        //modelBuilder.Entity<Person>().Property(p => p.RowVersion).HasComment("Versiyon kontrolü");
        #endregion
        #region ConcurrencyCheck
        //modelBuilder.Entity<Person>().Property(p => p.ConcurrencyCheck).IsConcurrencyToken();
        #endregion
        #region Composite Key
        //modelBuilder.Entity<Person>().HasKey("Id" , "Id2");
        //modelBuilder.Entity<Person>().HasKey(p=> new {p.Id,p.Id2});
        #endregion
        #region HasDefaultSchema
        //modelBuilder.HasDefaultSchema("defaultSchema");
        #endregion
        #region Property 
        #region HasDefaultValue
        //modelBuilder.Entity<Person>().Property(p=>p.Salary).HasDefaultValue(100);
        #endregion
        #region HasDefaultSqlValue
        //modelBuilder.Entity<Person>().Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");
        #endregion
        #endregion
        #region HasComputedColumn
        //modelBuilder.Entity<Example>().Property(p => p.Computed).HasComputedColumnSql("[x]+[y]");
        #endregion
        #region HasConstraintName
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.DepartmentId)
        //    .HasConstraintName("Constraint");
        #endregion
        #region HasData
        //modelBuilder.Entity<Department>().HasData(new Department() { Id=1,Name="Departman 2"});
        //modelBuilder
        //    .Entity<Person>()
        //    .HasData(new Person
        //    {
        //        Id = 1,
        //        Name = "Serkan",
        //        DepartmentId=1,
        //        Salary = 1000,
        //        CreatedDate = DateTime.Now
        //    }
        //    );
        #endregion
        #region HasDiscriminator
        //modelBuilder.Entity<Entity>().HasDiscriminator<int>("Ayırıcı").HasValue<A>(1).HasValue<B>(2).HasValue<Entity>(3);
        #endregion
        #region HasField
        //modelBuilder.Entity<Person>().Property(p => p.Name).HasField(nameof(Person._name));
        #endregion
        #region HasNoKey
        //modelBuilder.Entity<Example>().HasNoKey();
        #endregion
        #region HasIndex
        //modelBuilder.Entity<Person>().HasIndex(p => new
        //{
        //    p.Name,
        //    p.Surname
        //});
        #endregion
        #region HasQueryFilter
        modelBuilder.Entity<Person>().HasQueryFilter(p => p.CreatedDate.Year==DateTime.Now.Year); 
        #endregion
    }

}
public class Flight
{
    public int FlightID { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public string Name { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}

public class Airport
{
    public int AirportID { get; set; }
    public string Name { get; set; }
    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight> DepartingFlights { get; set; }

    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight> ArrivingFlights { get; set; }
}