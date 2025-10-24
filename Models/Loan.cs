using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Models
{
    public class Loan(string bookName, Guid readerId)
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [MaxLength(100)]
        public string BookName { get; private set; } = bookName;

        public DateTime ReturnDate { get; private set; } = DateTime.Now.AddDays(7);
        public bool Returned { get; private set; } = false;

        [Required]
        [ForeignKey("Reader")]
        public Guid ReaderId { get; private set; } = readerId;
        public Reader Reader { get; private set; } = null!;

        public void MarkAsReturned()
        {
            Returned = !Returned;
        }

        public void ExtendDate()
        {
            ReturnDate = ReturnDate.AddDays(7);
        }
    }
}
