using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Concrete;

namespace Udemy.TodoAppNTier.Business.Mapping.AutoMapper
{
    public class WorkProfile : Profile
    {
        //ctor
        public WorkProfile()
        {
            CreateMap<Work, WorkListDto>().ReverseMap();
            CreateMap<Work, WorkCreateDto>().ReverseMap();
            CreateMap<Work, WorkUpdateDto>().ReverseMap();
            //update controller action için.
            CreateMap<WorkListDto, WorkUpdateDto>().ReverseMap();
        }
    }
}
