using System.ComponentModel.DataAnnotations;

namespace Riwi.Api.Dtos
{
    public class CreateSpeakerDto
    {
        public long? PersonId { get; set; }

        public long? OrgId { get; set; }

        public string? Bio { get; set; }
    }

    public class UpdateSpeakerDto
    {
        public string? Bio { get; set; }
    }

    public class SpeakerDto
    {
        public long SpeakerId { get; set; }

        public long? PersonId { get; set; }

        public long? OrgId { get; set; }

        public string? Bio { get; set; }
    }
}
