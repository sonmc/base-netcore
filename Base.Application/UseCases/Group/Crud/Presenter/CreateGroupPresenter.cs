using AutoMapper;
using Base.Core.Schemas;
using System.ComponentModel.DataAnnotations; 

namespace Base.Application.UseCases
{
    public class CreateGroupPresenter
    {
        [Required]
        public string Title { get; set; } 
        [Required]
        public string ProfileType { get; set; }
     
        public string Description { get; set; }
    }

    public class CreateGroupMapping : Profile
    {
        public CreateGroupMapping()
        {
            // Default mapping when property names are same
            CreateMap<CreateGroupPresenter, GroupSchema>();

            // Mapping when property names are different
            //CreateMap<User, UserViewModel>()
            //    .ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName));
        }
    }
}
