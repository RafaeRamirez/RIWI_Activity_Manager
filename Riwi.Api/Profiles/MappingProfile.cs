using AutoMapper;
using Riwi.Api.Dtos;
using Riwi.Api.Models;

namespace Riwi.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Event Mappings
            CreateMap<CreateEventDto, Event>();
            CreateMap<UpdateEventDto, Event>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Event, EventDto>();

            CreateMap<CreateEventSessionDto, EventSession>();
            CreateMap<UpdateEventSessionDto, EventSession>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<EventSession, EventSessionDto>();

            CreateMap<CreateEventSpeakerDto, EventSpeaker>();
            CreateMap<EventSpeaker, EventSpeakerDto>();

            CreateMap<CreateEventTagDto, EventTag>();
            CreateMap<EventTag, EventTagDto>();

            CreateMap<CreateEventRequirementDto, EventRequirement>();
            CreateMap<UpdateEventRequirementDto, EventRequirement>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<EventRequirement, EventRequirementDto>();

            // Survey Mappings
            CreateMap<CreateSurveyTemplateDto, SurveyTemplate>();
            CreateMap<UpdateSurveyTemplateDto, SurveyTemplate>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<SurveyTemplate, SurveyTemplateDto>();

            CreateMap<CreateSurveyQuestionDto, SurveyQuestion>();
            CreateMap<UpdateSurveyQuestionDto, SurveyQuestion>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<SurveyQuestion, SurveyQuestionDto>();

            CreateMap<CreateSurveyResponseDto, SurveyResponse>();
            CreateMap<SurveyResponse, SurveyResponseDto>();

            CreateMap<CreateSurveyAnswerDto, SurveyAnswer>();
            CreateMap<SurveyAnswer, SurveyAnswerDto>();

            CreateMap<CreateEventSurveyDto, EventSurvey>();
            CreateMap<UpdateEventSurveyDto, EventSurvey>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<EventSurvey, EventSurveyDto>();

            // Other Core Mappings
            CreateMap<CreateAttendanceDto, Attendance>();
            CreateMap<Attendance, AttendanceDto>();

            CreateMap<CreateAuditLogDto, AuditLog>();
            CreateMap<AuditLog, AuditLogDto>();

            CreateMap<CreateCheckinTokenDto, CheckinToken>();
            CreateMap<CheckinToken, CheckinTokenDto>();

            CreateMap<CreateNotificationLogDto, NotificationLog>();
            CreateMap<NotificationLog, NotificationLogDto>();

            CreateMap<CreateOrganizationDto, Organization>();
            CreateMap<UpdateOrganizationDto, Organization>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Organization, OrganizationDto>();

            CreateMap<CreateRegistrationDto, Registration>();
            CreateMap<UpdateRegistrationDto, Registration>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Registration, RegistrationDto>();

            CreateMap<CreateSpeakerDto, Speaker>();
            CreateMap<UpdateSpeakerDto, Speaker>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Speaker, SpeakerDto>();

            CreateMap<CreateTagDto, Tag>();
            CreateMap<Tag, TagDto>();

            // Existing Models (if needed)
            CreateMap<CreatePersonDto, Person>();
            CreateMap<UpdatePersonDto, Person>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Person, PersonDto>();

            CreateMap<CreateCoderProfileDto, CoderProfile>();
            CreateMap<UpdateCoderProfileDto, CoderProfile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CoderProfile, CoderProfileDto>();

            CreateMap<CreateLocationDto, Location>();
            CreateMap<UpdateLocationDto, Location>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Location, LocationDto>();
        }
    }
}
