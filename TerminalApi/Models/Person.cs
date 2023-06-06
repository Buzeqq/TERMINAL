using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalApi.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("PersonId")]
        public int Id { get; set; }
        [Column("FirstName")]
        public required string FirstName { get; set; }
        [Column("LastName")]
        public required string LastName { get; set; }
    }
}
