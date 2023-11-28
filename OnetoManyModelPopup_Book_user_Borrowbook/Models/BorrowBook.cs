using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Models
{
    public class BorrowBook
    {
        [Key]
        public int BorrowBookId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Book Book { get; set; }  
    }
}
