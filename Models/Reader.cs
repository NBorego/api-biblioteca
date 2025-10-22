using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace APIBiblioteca.Models
{
    public class Reader(string name, string address, string phone)
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; } = name;
        [MaxLength(150)]
        public string Address { get; private set; } = address;

        [MaxLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O telefone deve conter exatamente 11 dígitos.")]
        public string Phone { get; private set; } = phone;
    }
}
