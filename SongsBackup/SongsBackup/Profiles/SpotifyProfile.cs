namespace SongsBackup.Profiles
{
    using AutoMapper;
    using Models.SpotifyModels;
    using Models.SpotifyModels.Dto;

    public class SpotifyProfile : Profile
    {
        public SpotifyProfile()
        {
            CreateMap<Items, SpotifyTrackDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => string.Join(", ", src.Artists.Select(a => a.Name))))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(opt => opt.AlbumArt, opt => opt.MapFrom(src => src.Album.Images.AsQueryable().FirstOrDefault(x => x.Height == 300)!.Url));
        }
    }
}