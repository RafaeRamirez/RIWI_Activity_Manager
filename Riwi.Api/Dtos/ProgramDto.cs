using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateProgrammDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateProgrammDto
    {
        public string? Name { get; set; }
    }

    public class ProgrammDto
    {
        public long ProgramId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
