using Riwi.Api.Enums;
using System.ComponentModel.DataAnnotations;


namespace Riwi.Api.Models;

public class SurveyTemplate
{
    [Key]
    public long TemplateId { get; set; }          // BIGSERIAL → long

    public string Name { get; set; } = null!;     // NOT NULL

    public SurveyPurpose Purpose { get; set; }    // purpose CHECK ('satisfaction','impact')

    public bool IsActive { get; set; } = true;    // DEFAULT TRUE

    public ICollection<SurveyResponse> Responses { get; set; } = new List<SurveyResponse>();
}
