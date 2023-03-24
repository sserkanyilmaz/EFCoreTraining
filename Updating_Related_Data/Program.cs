using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

ApplicationDbContext context = new();
#region One To One İlişisel senaryolarda Veri Güncelleme

#region Saving
//Person person = new()
//{
//    Name = "Serkan",
//    Address = new Address()
//    {
//        PersonAddress = "Pendik/İst"
//    }
//};
//Person person2= new()
//{
//    Name = "Hilmi"
////};
//await context.AddAsync(person);
//await context.AddAsync(person2);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımli veriyi değiştirme

//Person? person = await context.Persons.Include(a=>a.Address).FirstOrDefaultAsync(p=>p.Id==1);

//context.Addresses.Remove(person.Address);
//person.Address = new()
//{
//    PersonAddress = "Yeni Adres"
//};
//await context.SaveChangesAsync();

#endregion
#region 2. Durum | Esas tablodaki veriye bağımli veriyi değiştirme

//Address? address = await context.Addresses.FindAsync(1);
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();
//Person? person = await context.Persons.FindAsync(2);
//address.Person = person;

//await context.Addresses.AddAsync(address);
//await context.SaveChangesAsync();
#endregion
#endregion

#region One to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Blog blog = new()
//{
//    Name = "Serkan.blogspot",
//    Posts = new List<Post>
//    {
//        new(){Title="1.post"},
//        new(){Title="2.post"},
//        new(){Title="3.post"}
//    }
//};
//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion

#region 1.Durum |Esas tablokdaki veriye bağımlı verileri değiştirme
//Blog? blog = await context.Blogs.Include(b=>b.Posts).FirstOrDefaultAsync(b => b.Id == 1);

//Post? silinecekPost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(silinecekPost);
//blog.Posts.Add(new() { Title = "4.post" });
//blog.Posts.Add(new() { Title = "5 .post" });

//await context.SaveChangesAsync();
#endregion
#region 2.Durum | Bağımlı verilerin ilişkisel oldupu ana veriyi güncelleme
//Post? post = await context.Posts.FindAsync(4);
//post.Blog = new Blog()
//{
//    Name = "ikinci blog"
//};
//await context.SaveChangesAsync();
//Post? post = await context.Posts.FindAsync(5);
//Blog? blog = await context.Blogs.FindAsync(2);
//post.Blog = blog;
//await context.SaveChangesAsync();
#endregion
#endregion

#region Many To Many  İlişisel senaryolarda Veri Güncelleme


#region Saving

//Book book1 = new() { BookName = "1.Kitap" };
//Book book3 = new() { BookName = "3.Kitap" };
//Book book2 = new() { BookName = "2.Kitap" };

//Author author1 = new() { AuthorName = "1.Yazar" };
//Author author2 = new() { AuthorName = "2.Yazar" };
//Author author3 = new() { AuthorName = "3.Yazar" };
//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);

//await context.SaveChangesAsync();
#endregion

#region 1.Örnek
//Book? book = await context.Books.FindAsync(1);

//Author? author = await context.Authors.FindAsync(3);

//book.Authors.Add(author);

//await context.SaveChangesAsync(); 


#endregion
#region 2. Örnek

//Author? author = await context.Authors.Include(x=>x.Books).FirstOrDefaultAsync(y=>y.Id);

//foreach (var item in author.Books)
//{
//    if(item.Id!=1)
//        author.Books.Remove(item);
//}

//await context.SaveChangesAsync();  
#endregion
#region 3.Örnek
//1 idsine ilişkisini kes 3.yazara ekle 4.yazarı da ekle
//Book? book = context.Books.Include(y=>y.Authors).SingleOrDefault(b=>b.Id==1);

//Author a = book.Authors.FirstOrDefault(b=>b.Id==1);
//book.Authors.Remove(a);

//book.Authors.Add(context.Authors.Find(3));
//book.Authors.Add(new() { AuthorName="4.Yazar "});


#endregion
#endregion


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=EApplicationDB;trusted_connection=true;trustservercertificate=true;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}