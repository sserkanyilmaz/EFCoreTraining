using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

Console.WriteLine("dsfghj");
ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem -> Principal Entity Üzerinden Dependent Entity Verisi Ekleme
//Person person = new Person();
//person.Name = "Serkan";
//person.Address = new() { PersonAddress = "Pendik İstanbul" };

//await context.AddAsync(person);
//await context.SaveChangesAsync();
#endregion

//Eğer ki principal entity üzerinden ekleme gerçekleştiriliyorsa dependent entity nesnesi verilmek zorunda değildir! amma velakin
//dependent entity üzerinden ekleme işlemi gerçekleştiriliyorsa eğer burada principal entitynin nesnesine ihtiyacımız zaruridir.
#region 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Address address = new()
//{
//    PersonAddress = "Onikişubat ",
//    Person = new() { Name = "Serkan" }
//};

//await context.AddAsync(address);
//await context.SaveChangesAsync();
#endregion

//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public Address Address { get; set; }
//}
//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }

//    public Person Person { get; set; }
//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EProjeDB;Trusted_Connection=True;TrustServerCertificate=True;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);
//    }
//}
#endregion

#region One to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem -> Principal Entity Üzerinden Dependent Entity Verisi Ekleme

#region Nesne Referansı Üzerinden Ekleme
//Blog blog = new Blog();
//blog.Name = "Test";
//blog.Posts.Add(new() { Title = "post 1" });
//blog.Posts.Add(new() { Title = "post 2" });
//blog.Posts.Add(new() { Title = "post 3" });

//await context.AddAsync(blog); 
//await context.SaveChangesAsync();
#endregion

#region Object Initializer Üzerinden Ekleme
//Blog blok2 = new Blog() { 
//Name = "Test",
//Posts = new HashSet<Post>() { new Post
//    {
//        Title= "post 4"
//    } ,
//    new Post
//    {
//        Title= "post 5"
//    },
//    new Post
//    {
//        Title= "post 6"
//    }
//}
//};
//await context.AddAsync(blok2);
//await context.SaveChangesAsync();

#endregion

#endregion

#region 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Post post = new Post()
//{
//    Title="Baslik",
//    Blog = new()
//    {
//        Name="new blog"
//    }
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();
#endregion

#region 3. Yöntem -> Foreign Key Üzerinden Principal Entity Verisi Ekleme
//1. ve 2. yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken, 
//bu 3. yöntem önceden eklenmiş bir principal entity verisiyle yeni dependent entitylerin ilişkisel olarak eşleştirilmeisni sağlamaktadır.

//Post post = new Post()
//{
//    Title = "post 7",
//    BlogId = 1,
//};
//await context.AddAsync(post);
//await context.SaveChangesAsync();
#endregion

//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Post> Posts { get; set; }
//}
//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }

//    public Blog Blog { get; set; }
//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Post> Posts { get; set; }
//    public DbSet<Blog> Blogs { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EProjeDB;Trusted_Connection=True;TrustServerCertificate=True;");
//    }
//}
#endregion

#region Many to Many İlişkisel Senaryolarda Veri Ekleme
#region 1. Yöntem
//many to many ilişkisinde eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir
//Book book = new Book()
//{
//    BookName= "kitap 1",
//    Authors = new HashSet<Author>()
//    {
//        new Author(){AuthorName = "Author 1"},
//        new Author(){AuthorName = "Author 2"},
//        new Author(){AuthorName = "Author 3"}
//    }
//};

//await context.AddAsync(book);
//await context.SaveChangesAsync();
//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<Author> Authors { get; set; }
//}

//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }

//    public ICollection<Book> Books { get; set; }
//}
#endregion
#region 2. Yöntem
//many to many ilişkisinde eğer ki Fluent API üzerinden tasarlanmışsa kullanılan bir yöntemdirm

Author author = new Author() 
{ 
    AuthorName="Serkan",
    Books = new HashSet<BookAuthor>()
    {
        new BookAuthor(){ AuthorId=1},
        new BookAuthor(){ Book = new(){ BookName="B"} }
    }
};
await context.AddAsync(author);
await context.SaveChangesAsync();
class Book
{
    public Book()
    {
        Authors = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<BookAuthor> Authors { get; set; }
}

class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<BookAuthor> Books { get; set; }
}
#endregion



class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
    {
        optionsbuilder.UseSqlServer("server=localhost\\sqlexpress;database=EApplicationDB;trusted_connection=true;trustservercertificate=true;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.AuthorId, ba.BookId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }
}
#endregion