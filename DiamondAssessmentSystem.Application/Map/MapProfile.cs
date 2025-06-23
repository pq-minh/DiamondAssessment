using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            //// Ánh xạ giữa Order (trước đây là Booking) và OrderDto (trước đây là BookingDto)
            //CreateMap<Order, OrderDto>()
            //    .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            //    .ForMember(dest => dest.Commitment, opt => opt.MapFrom(src => src.Commitment))
            //    .ForMember(dest => dest.Consultant, opt => opt.MapFrom(src => src.Consultant))
            //    .ForMember(dest => dest.Receipt, opt => opt.MapFrom(src => src.Receipt))
            //    .ForMember(dest => dest.Sealing, opt => opt.MapFrom(src => src.Sealing));

            //CreateMap<OrderCreateDto, Order>();

            //// Ánh xạ giữa CustomerCreateDto và Customer
            //CreateMap<CustomerCreateDto, Customer>();

            //// Ánh xạ giữa Certificate và CertificateDto
            //CreateMap<Certificate, CertificateDto>()
            //    .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Results));

            //// Ánh xạ giữa CertificateCreateDto và Certificate
            //CreateMap<CertificateCreateDto, Certificate>();

            //CreateMap<OrderDetail, OrderDetailDto>()
            //    .ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service))
            //    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result));


            //CreateMap<OrderDetailCreateDto, OrderDetail>();

            CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));

            CreateMap<User, AccountDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Request, RequestCreateDto>().ReverseMap();

            CreateMap<ServicePrice, ServicePriceCreateDto>().ReverseMap();
            CreateMap<ServicePrice, ServicePriceDto>().ReverseMap();

            CreateMap<Result, ResultDto>().ReverseMap();
            CreateMap<Result, ResultCreateDto>().ReverseMap();

        }
    }
}
