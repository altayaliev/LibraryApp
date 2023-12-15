using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Book name could not be blank ")]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Author could not be blank ")]
        public string Author { get; set; }
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Price could not be blank ")]
        public float Price { get; set; }

        [DisplayName("Book Category")]
        [Required(ErrorMessage = "Book Type could not be blank ")]
        public int BookTypeId { get; set; }
        [ValidateNever]
        public BookType? BookType { get; set; }

        public string? PictureURL  { get; set; }
    }
}
