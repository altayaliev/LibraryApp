using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int BookId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }

    }
}
