using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnetoManyModelPopup_Book_user_Borrowbook.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public ICollection<BorrowBook> BorrowBooks { get; set; }
    }
}
