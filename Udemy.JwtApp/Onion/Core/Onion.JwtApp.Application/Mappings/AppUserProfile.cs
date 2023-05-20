using AutoMapper;
using Onion.JwtApp.Application.Dtos;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, CreatedUserDto>().ReverseMap();
        }
    }
}
