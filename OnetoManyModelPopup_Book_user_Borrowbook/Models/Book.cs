using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public double ISBN { get; set; }
        [JsonIgnore]
        public ICollection<BorrowBook> BorrowBooks { get; set; }

    }
}
