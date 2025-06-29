using AutoMapper;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Infrastructure.Models;

namespace DiamondAssessmentSystem.Application.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //CreateMap<Order, OrderDto>()
            //    .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            //    .ForMember(dest => dest.Commitment, opt => opt.MapFrom(src => src.Commitment))
            //    .ForMember(dest => dest.Consultant, opt => opt.MapFrom(src => src.Consultant))
            //    .ForMember(dest => dest.Receipt, opt => opt.MapFrom(src => src.Receipt))
            //    .ForMember(dest => dest.Sealing, opt => opt.MapFrom(src => src.Sealing));

            //CreateMap<OrderCreateDto, Order>();

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

            CreateMap<RegisterEmployeesDto, User>()
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

            CreateMap<CustomerCreateDto, Customer>()
                .ForMember(dest => dest.Idcard, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.IdCard) ? (decimal?)null : decimal.Parse(src.IdCard)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
                .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src => src.TaxCode));

            CreateMap<CustomerCreateDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.User.Point))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.User.Note))
                .ForMember(dest => dest.IdCard, opt => opt.MapFrom(src => src.Idcard.ToString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
                .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src => src.TaxCode));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender));

            CreateMap<Certificate, CertificateDto>().ReverseMap();
            CreateMap<Certificate, CertificateCreateDto>().ReverseMap();

            CreateMap<Blog, BlogDto>().ReverseMap();

        }
    }
}
