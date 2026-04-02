using Riwi.Api.Dtos;

namespace Riwi.Api.Interfaces
{
    public interface ISurveyService
    {
        // Templates
        Task<SurveyTemplateDto> CreateTemplateAsync(CreateSurveyTemplateDto dto);
        Task<SurveyTemplateDto?> GetTemplateByIdAsync(long id);
        Task<IEnumerable<SurveyTemplateDto>> GetAllTemplatesAsync();
        
        // Questions
        Task<SurveyQuestionDto> AddQuestionAsync(CreateSurveyQuestionDto dto);
        Task<IEnumerable<SurveyQuestionDto>> GetQuestionsByTemplateIdAsync(long templateId);
        
        // Responses
        Task<SurveyResponseDto> SubmitResponseAsync(CreateSurveyResponseDto dto, IEnumerable<CreateSurveyAnswerDto> answers);
        Task<IEnumerable<SurveyResponseDto>> GetResponsesByEventIdAsync(long eventId);
    }
}
