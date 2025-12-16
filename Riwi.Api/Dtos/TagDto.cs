using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }

    public class TagDto
    {
        public long TagId { get; set; }

        public string Name { get; set; } = null!;
    }
}
