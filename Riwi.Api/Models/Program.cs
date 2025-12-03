using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Riwi.Api.Models
{
    public class Program
    {
        [Key]
        public long ProgramId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
