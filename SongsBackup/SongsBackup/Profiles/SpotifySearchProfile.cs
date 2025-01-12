namespace SongsBackup.Profiles
{
    using AutoMapper;
    using Models.SpotifyModels.Dto;
    using Models.SpotifyModels.SubModels;

    public class SpotifySearchProfile : Profile
    {
        public SpotifySearchProfile()
        {
            CreateMap<Items, SongSearchResultDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => string.Join(", ", src.Artists.Select(a => a.Name))))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.AlbumArt, opt => opt.MapFrom(src => src.Album.Images.AsQueryable().FirstOrDefault(x => x.Height == 300)!.Url))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));
        }
    }
}