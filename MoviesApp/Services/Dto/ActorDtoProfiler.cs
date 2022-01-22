using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels.ActorViewModels;

namespace MoviesApp.Services.Dto
{
    public class ActorDtoProfile:Profile
    {
        public ActorDtoProfile()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorDto, ActorViewModel>().ReverseMap();
            CreateMap<ActorDto, EditActorViewModel>().ReverseMap();
            CreateMap<Actor, EditActorViewModel>().ReverseMap();
            CreateMap<ActorDto, InputActorViewModel>().ReverseMap();
            CreateMap<ActorDto, DeleteActorViewModel>().ReverseMap();
        }
    }
}