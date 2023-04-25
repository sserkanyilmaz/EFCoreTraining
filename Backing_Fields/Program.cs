using Microsoft.EntityFrameworkCore;

BackingFieldDbContext context= new BackingFieldDbContext();
var person = await context.Persons.FindAsync(1);
//Person person2 = new()
//{
//    Name = "Person 101",
//    Department = "Department 101"
//};
//await context.Persons.AddAsync(person);


Console.ReadLine();
#region Backing Fields
//Tablo içerisindeki kolonları entity classları içerisinde propertyler ile değil fieldlarla temsil etmemizi sağlayan bir özelliktir.

//public class Person
//{
//    public int Id { get; set; }
//    public string? name;
//    public string? Name { get => name.Substring(0,3); set=>name = value; }
//    public string? Department { get; set; } 
//}
#endregion

#region BackingField  Attributes
//public class Person
//{
//    public int Id { get; set; }
//    public string? name;
//    [BackingField(nameof(name))]
//    public string? Name { get; set; }
//    public string? Department { get; set; }
//}
#endregion

#region HasFields Fluent API 
//Fluent API'da HasField metodu BackingField özelliğine karşılık gelmektedir.
//public class Person
//{
//    public int Id { get; set; }
//    public string? name;
//    public string? Name { get ; set; }
//    public string? Department { get; set; } 
//}
#endregion

#region Field And Propert Access
//EF Core sorgulama sürecinde enttiiy içerisindeki propertyleri ya da field'ları kullanıp kullanmayacağının davranışını bizlere belirtmektedir.

//EF Core, hiçbir ayarlama yoksa varsayılan olarak propertyler üzerinden verileri işler, eğer ki backing field bildiriliyorsa
//field üzerinden işler yok eğer backing field bildirildiği halde davranış belirtiliyorsa ne belirtilmişse ona göre işlemeyi devam ettirir.


//UsePropertyAccessMode üzerinden davranıl modellemesi gerçekleştirebilir

#endregion
#region Field-Only Properties
//Entitylerde değerleri almak için property'ler yerine metotların kullanıldığı veya belirli alanların hiç gösterilmemesi gerektiği 
//durumlarda(örneğin primary key kolonu) kullanılabilir

public class Person
{
    public int Id { get; set; }
    public string? name;
    public string? Department { get; set; }
    public string GetName() => name;
    public string SetName(string value) => name=value;
}
#endregion
public class BackingFieldDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //    modelBuilder.Entity<Person>()
        //        .Property(p => p.name)
        //        .HasField(nameof(Person.name))
        //        .UsePropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.Entity<Person>().Property(nameof(Person.name));
    }
    /*
    Field = veri erişim süreçlerinde sadece field'ların kullanılmasını söyler. 
    Eğer field'ın kullanılamayacağı durum söz konusu olursa bir exception fırlatır.

    FieldDuringConstruction = Veri erişim süreçlerinde ilgili entityden bir nesne oluşturulma sürecinde field'ların kullanılmasını söyler

    Property = Veri erişim sürecine sadece propertynin kullanılmasını söyler. Eğer property'nin kullanılamayacağı durum söz konusuysa (read-only, write-only) bit exception fırlatır.

    PreferField 
    PreferFieldDuringConstruction 
    PreferProperty
     
     */


}