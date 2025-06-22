using AutoMapper;
using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using Demo.Web.Areas.Admin.Models;

namespace Demo.Api
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<AddAuthorModel, Author>().ReverseMap();
            CreateMap<UpdateAuthorModel, Author>().ReverseMap();
            CreateMap<AuthorSearchModel, AuthorSearchDto>().ReverseMap();
        }
    }
}
