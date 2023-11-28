using Microsoft.EntityFrameworkCore;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options): base(options) { }

        public DbSet<Book> Books { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowBook> BorrowBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowBook>()
                .HasOne(a => a.Book)
                .WithMany(d => d.BorrowBooks)
                .HasForeignKey(d => d.BookId)
                .IsRequired();

            modelBuilder.Entity<BorrowBook>()
                .HasOne(d=>d.User)
                .WithMany(d=>d.BorrowBooks)
                .HasForeignKey(d=>d.UserId)
                .IsRequired();  
        }
    }
}
