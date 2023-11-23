using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new ApplicationDbContext();



#region Table Per Type(TPT) Nedir?
//Entitylerin aralarında kalıtımsal ilişkiye sahip olduğu durumlarda her bir türe/entitye/tipe/referansa karşılık bir tablo generate edern davranıştır.
//Her generate edilen bu tablolar hiyerarşik düzlemde kendi aralarında birebir ilişkiye sahiptir.

#endregion

#region TPT Nasıl Uygulanır
/*
 * TPT yi uygulayabilmek için öncelikle entityleri kendi aralarında olması gereken mantıkta inşa edilmesi gerekmektedir.
 * 
 * Entityler DbSet olarak bildirilmelidir.
 * 
 * Hiyerarşik olarak aralarında katılımsal  ilişki olan tüm entityleri OnModelCreating fonksiyonunda ToTable metodu ile konfigüre edilmelidir.
 * Böylece EF Core kalıtımsal ilişki olan bu tablolar arasında TPT davranışı olacağını anlayacaktır.
 * */
#endregion

#region TPT'de Veri Ekleme
//davranışların hiçbirinde veri eklerken , silerken,güncellerken vs normal operasyonların dışında bir işlem yapılmaz.
//Hangi davranışı kullanıyorsanız EF Core ona göre arkaplanda modellemeyi gerçekleştirecektir.

//Technician t1 = new Technician() { Name = "Technician Serkan", Surname = "YIlmaz", Department = "Department", Branch = "Branch" };
//Technician t2= new Technician() { Name = "Technician Zafer", Surname = "YIlmaz", Department = "Department", Branch = "Branch" };
//Technician t3 = new Technician() { Name = "Technician Mehmet", Surname = "YIlmaz", Department = "Department", Branch = "Branch" };
//await context.Technicians.AddAsync(t1);
//await context.Technicians.AddAsync(t2);
//await context.Technicians.AddAsync(t3);

//Customer customer = new Customer() { Name = "hilmi", Surname = "celayir", CompanyName = "şirket" };;
//await context.Customers.AddAsync(customer);
//await context.SaveChangesAsync();
#endregion

#region TPT'de Veri Silme
//var emp =await context.Employees.FindAsync(3);
//context.Employees.Remove(emp); 
//await context.SaveChangesAsync();

#endregion

#region TPT'de Veri Güncelleme
var emp=await context.Employees.FindAsync(1);
emp.Name = "asdasdasd";
await context.SaveChangesAsync();
#endregion

#region TPT'de Veri Sorgulama  

var technicians = await context.Technicians.ToListAsync();
var employees= await context.Employees.ToListAsync();
#endregion



public abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

}
public class Employee : Person
{
    public string? Department { get; set; }
}

public class Customer : Person
{
    public string? CompanyName { get; set; }
}
public class Technician : Employee
{
    public string? Branch { get; set; }
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TPT ;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Technician>().ToTable("Technicians");
    }
}