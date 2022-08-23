using AutoMapper;
using Camp.Common.DTOs;
using Camp.Common.Models;
using Camp.DataAccess.Entities;

namespace Camp.Common.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserModel, User>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(
                dest => dest.Login,
                opt => opt.MapFrom(src => src.Login))
                .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password))
                .ForMember(
                dest => dest.ParentId,
                opt => opt.MapFrom(src => src.ParentId))
                .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => src.Location))
                .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<User, UserDto>()
                .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));

            CreateMap<User, ProfileDto>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => src.Location))
                .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(
                dest => dest.IsVerify,
                opt => opt.MapFrom(src => src.IsVerify))
                .ForMember(
                dest => dest.CreateDate,
                opt => opt.MapFrom(src => src.CreateDate.ToUnixTimeSeconds()));
        }
    }
}
