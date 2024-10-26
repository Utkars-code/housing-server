using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using webApi_build_Real.Dto;
using webApi_build_Real.Models;

namespace webApi_build_Real.Helper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap(); 
        }
    }
}
