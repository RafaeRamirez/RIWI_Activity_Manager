using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riwi.Api.Models
{
    [Table("programs")]
    public class ProgramEntity
    {
        [Key]
        [Column("program_id")]
        public long ProgramId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}