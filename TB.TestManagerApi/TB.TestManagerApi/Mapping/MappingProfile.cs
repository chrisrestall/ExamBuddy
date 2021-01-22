using AutoMapper;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ExamMaster, ExamMasterDto>();
            CreateMap<ExamMasterDto, ExamMaster>();
            CreateMap<CreateExamMasterDto, CreateExamMaster>();
            CreateMap<UpdateExamMasterDto, UpdateExamMaster>();
            CreateMap<UpdateExamQuestionMasterDto, UpdateExamQuestionMaster>();
            CreateMap<UpdateExamAnswerMasterDto, UpdateExamAnswerMaster>();            
            CreateMap<DeactivateExamMasterDto, DeactivateExamMaster>();
            CreateMap<ExamTypeMeta, ExamTypeMetaDto>().ReverseMap();
            CreateMap<ExamTypeSection, ExamTypeSectionDto>().ReverseMap();
            CreateMap<CreateExamTypeMetaDto, CreateExamTypeMeta>();
            CreateMap<CreateExamTypeSectionDto, CreateExamTypeSection>();
            CreateMap<UpdateExamTypeMetaDto, UpdateExamTypeMeta>();
            CreateMap<UpdateExamTypeSectionDto, UpdateExamTypeSection>();
            CreateMap<UpdateExamAnswerQuestionXrefCorrectDto, UpdateExamAnswerQuestionXrefCorrect>();
            CreateMap<DeactivateExamTypeMetaDto, DeactivateExamTypeMeta>();
            CreateMap<DeactivateExamTypeSectionDto, DeactivateExamTypeSection>();
            CreateMap<CreateExamAnswerMasterDto, CreateExamAnswerMaster>();
            CreateMap<CreateExamQuestionDto, CreateExamQuestion>();
            CreateMap<CreateExamQuestionMasterDto, CreateExamQuestionMaster>();
            CreateMap<CreateExamAnswerDto, CreateExamAnswer>();
            CreateMap<ExamQuestionAnswer, ExamQuestionAnswerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExamQuestionAnswerXrefId)); 
            CreateMap<ExamAnswerMaster, ExamAnswerMasterDto>();
            CreateMap<ExamQuestionMaster, ExamQuestionMasterDto>();
            CreateMap<ExamQuestion, ExamQuestionDto>()
                .ForPath(dest => dest.TestQuestionAnswer.ExamAnswer, opt => opt.MapFrom(src => src.ExamAnswerMaster))
                .ForPath(dest => dest.TestQuestionAnswer.ExamQuestion, opt => opt.MapFrom(src => src.ExamQuestionMaster))
                .ForPath(dest => dest.TestQuestionAnswer.Id, opt => opt.MapFrom(src => src.ExamQuestionAnswerXref.Id))
                .ForPath(dest => dest.TestQuestionAnswer.IsCorrect, opt => opt.MapFrom(src => src.ExamQuestionAnswerXref.isCorrect));

        }
    }
}
