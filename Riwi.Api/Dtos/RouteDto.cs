using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateRouteDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateRouteDto
    {
        public string? Name { get; set; }
    }

    public class RouteDto
    {
        public long ProgramId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
