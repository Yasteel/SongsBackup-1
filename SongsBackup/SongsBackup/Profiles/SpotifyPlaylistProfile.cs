using SongsBackup.Models.SpotifyModels.Dto;
using SongsBackup.Models.SpotifyModels.SubModels;

namespace SongsBackup.Profiles
{
    using AutoMapper;

    public class SpotifyPlaylistProfile : Profile
    {
        public SpotifyPlaylistProfile()
        {
            CreateMap<Items, UserPlaylistDto>();
        }
    }
}