using AutoMapper;
using Demo.Domain.Entities;
using Demo.Web.Areas.Admin.Models;

namespace Demo.Web
{
    public class WebProfile : Profile
    {
        public WebProfile() 
        {
            CreateMap<AddAuthorModel, Author>().ReverseMap();
        }
    }
}
