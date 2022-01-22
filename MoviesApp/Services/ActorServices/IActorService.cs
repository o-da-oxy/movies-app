using System.Collections.Generic;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services.ActorServices
{
    public interface IActorService
    {
        ActorDto GetActor(int id);
        IEnumerable<ActorDto> GetAllActors();
        ActorDto UpdateActor(ActorDto actorDto);
        ActorDto AddActor(ActorDto actorDto);
        ActorDto DeleteActor(int id);
    }
}