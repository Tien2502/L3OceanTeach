using AutoMapper;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Models.ViewModel;

namespace BTL3_MVT.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Ward, WardDTO>().ReverseMap();
            CreateMap<Ethnicity, EthnicityDTO>().ReverseMap();
            CreateMap<Work, WorkDTO>().ReverseMap();
            //
            CreateMap<Person, PersonVM>()
                .ForMember(dest => dest.Age, otp => otp.MapFrom(src => CalculateAge(src.BirthDay)))
                .ForMember(dest => dest.Work, otp => otp.MapFrom(src => src.Work.WorkName))
                .ForMember(dest => dest.Ethnicity, otp => otp.MapFrom(src => src.Ethnicity.EthnicityName))
                .ForMember(dest => dest.City, otp => otp.MapFrom(src => src.City.CityName))
                .ForMember(dest => dest.District, otp => otp.MapFrom(src => src.District.DistrictName))
                .ForMember(dest => dest.Ward, otp => otp.MapFrom(src => src.Ward.WardName))
                .ForMember(dest => dest.WorkCity, otp => otp.MapFrom(src => src.WorkCity.CityName))
                .ForMember(dest => dest.WorkDistrict, otp => otp.MapFrom(src => src.WorkDistrict.DistrictName))
                .ForMember(dest => dest.WorkWard, otp => otp.MapFrom(src => src.WorkWard.WardName));
        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            var currentDate = DateTime.Today;
            var age = currentDate.Year - dateOfBirth.Year;

            if (currentDate < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
