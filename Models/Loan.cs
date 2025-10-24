using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Models
{
    public class Loan(string bookName, DateTime returnDate, Guid readerId)
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [MaxLength(100)]
        public string BookName { get; private set; } = bookName;
        [Required]
        public DateTime ReturnDate { get; private set; } = returnDate;
        public bool Returned { get; private set; } = false;

        [Required]
        [ForeignKey("Reader")]
        public Guid ReaderId { get; private set; } = readerId;
        public Reader Reader { get; private set; } = null!;

        public void MarkAsReturned()
        {
            Returned = !Returned;
        }

        internal void ExtendDate()
        {
            ReturnDate.AddDays(7);
        }
    }
}
