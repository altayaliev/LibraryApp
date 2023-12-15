using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class RentOrder
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentNo { get; set; }
        [DefaultValue ("false")]
        public bool OrderState { get; set; }
        public bool CreatedRent { get; set; } = false;
        public Book? Book { get; set; }

    }
}
