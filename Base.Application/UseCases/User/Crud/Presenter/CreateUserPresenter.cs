using AutoMapper;
using Base.Core.Schemas;
using System.ComponentModel.DataAnnotations; 

namespace Base.Application.UseCases
{
    public class CreateUserPresenter
    {
        [Required]
        public string Username { get; set; } 
        public string? Email { get; set; }
        [Required]
        public string GroupIds { get; set; }
    }

    public class CreateUserMapping : Profile
    {
        public CreateUserMapping()
        {
            // Default mapping when property names are same
            CreateMap<CreateUserPresenter, UserSchema>();

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
