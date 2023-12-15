using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class BookType
    {
        [Key] // primary key
        public int Id { get; set; }
        [Required(ErrorMessage = "Book type name could not be blank ")]  // not null
        [MaxLength(25)]
        [DisplayName("Book type name")]
        public string Name { get; set; }
        List<Book> books { get; set;}
    }
}
