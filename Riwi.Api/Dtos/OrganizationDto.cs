using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateOrganizationDto
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Kind { get; set; }

        public string? Website { get; set; }
    }

    public class UpdateOrganizationDto
    {
        public string? Name { get; set; }

        public string? Kind { get; set; }

        public string? Website { get; set; }
    }

    public class OrganizationDto
    {
        public long OrgId { get; set; }

        public string Name { get; set; } = null!;

        public string? Kind { get; set; }

        public string? Website { get; set; }
    }
}
