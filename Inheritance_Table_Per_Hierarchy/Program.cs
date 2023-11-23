// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
ApplicationDbContext context = new ApplicationDbContext();  
Console.WriteLine("Hello, World!");
#region Table Per Hierarchy (TPH) Nedir?
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiy ebir tablo oluşturan davranıştır.


#endregion
#region Neden Table Per Hierarchy Yaklaşımında Bir Tabloya İhtiyacımız Olsun?
//İçierisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa
//bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirlerinden ayırabiliriz.
//bu tarz bi tablonun oluşturulması ve bu tarz tabloya göre sorgulama , veri ekleme, silme vs. gibi operasyonların şekillendirilmesi için 
//TPH davranışını kullanabiliriz.
#endregion
#region TPH Nasıl Uygulanır
//EF Coreda entityler arasında temel bir kalırımsal ilişki söz konusuysa default olarak kabul edilen davranıştır.

//o yüzden herhangi bir konfigürasyon gerektirmez.

//Entitler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir.
#endregion
#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklaşımı neticesinde kümülatif olarak inşa endilmeiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar.
//Discriminator kolonunu komple özelleştirilebilir
#endregion
#region Discriminator Kolonu Nedir?
//Öncelikle hiyerarşinin başında hangi sınıf varsa onun Fluent API'da onun konfigürasyonuna gidilmeli
//Ardından HasDiscriminator fonksiyonu ile özelleştirilmeli
#endregion

//ApplicationDbContext context = new ApplicationDbContext();
//Employee employee = new Employee();
//employee.Name = "Serkan";
//employee.Surname = "yılmaz";
//await context.Employees.AddAsync(employee);
//await context.SaveChangesAsync();
#region Discriminator değeri nasıl değiştirilir?
//Hiyerarşinin başındaki Entity konfigurasyonlarına gelip HasDiscriminator fonkisyonu ile özelleştirmede bulunarak ardından HasValue fonksiyonu ile hangi entitye karşılık hangi değerin girilecegini belirtebiliriz
#endregion


#region TPH'da Veri Ekleme
//davranışların hiçbirinde veri eklerken , silerken,güncellerken vs normal operasyonların dışında bir işlem yapılmaz.
//Hangi davranışı kullanıyorsanız EF Core ona göre arkaplanda modellemeyi gerçekleştirecektir.

//Employee e1 = new Employee() { Name = "Employee Serkan", Surname = "Yılmaz", Department = "Department" };
//Employee e2 = new Employee() { Name = "Employee Serkan2", Surname = "Yılmaz2", Department = "Department" };
//Customer c1 = new Customer() { Name = "Customer Serkan3", Surname = "Yılmaz3", CompanyName="Company"};
//Customer c2 = new Customer() { Name = "Customer Serkan4", Surname = "Yılmaz4", CompanyName="Company2"};
//Technician t1 = new Technician() { Name = "Technician Serkan", Surname = "YIlmaz", Department = "Department", Branch = "Branch" };
//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);
//await context.SaveChangesAsync();

#endregion

#region TPH'da Veri Silme
//TPH davranışında silme operasyonu yine entity üzerinden gerçekleştirilir.
//var emp=await context.Employees.FindAsync(1);
//context.Employees.Remove(emp);
//await context.SaveChangesAsync();

//var customers = await context.Customers.ToListAsync();
//context.Customers.RemoveRange(customers);
//await context.SaveChangesAsync();

#endregion

#region TPH'da Veri Güncelleme
//TPH davranışında güncelleme operasyonu yine entity üzerinden gerçekleşir
//var emp = await context.Employees.FindAsync(4);
//emp.Name = "serkakkn";
//await context.SaveChangesAsync();
#endregion

#region TPH'da Veri Sorgulama
//Veri sorgulama operasyonu bilinen dbset propertysi üzerinden sorgulamadır. ancak dikkat edilmesi gerekir ki 

//var emps= await context.Employees.ToListAsync();
//var  tecs= await context.Technicians.ToListAsync();

//kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileri de kapsamaktadır. O Yüzden üst sınıfların sorgularında alt sınıfların da verisi gelecektir.
//Sorgulama süreçlerinde EF Core genetare edilen sorguya bir Where şartı eklemektedir.
#endregion

#region Farklı Entitylerde Aynı İsimde Sütunların Olduğu Durumlar
//Entitylerde mükerrer kolonlar olabilir. bu kolonları EF Core isimsel olarak özelleştirip ayıracaktır.
#endregion








public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

}
public class Employee: Person
{
    public string? Department { get; set; }

}

public class Customer : Person
{
    public string? CompanyName { get; set; }

}
public class Technician :Employee
{
    public string? Branch { get; set; }
}
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>().HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BackingFieldDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}