using AutoMapper;
using SongsBackup.Models.SpotifyModels.Dto;

namespace SongsBackup.Services
{
    using Newtonsoft.Json;
    using Interfaces;
    using Models;
    using Models.SpotifyModels;
    using Models.SpotifyModels.RequestModel;
    using Models.SpotifyModels.SubModels;
    using System.Text;
    using System.Net.Http.Headers;

    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SpotifyService(IHttpClientFactory clientFactory, ISessionService sessionService, IMapper mapper)
        {
            _clientFactory = clientFactory;
            _sessionService = sessionService;
            _mapper = mapper;
        }
        
        public async Task<ProfileResponse?> GetProfile()
        {
            var token = _sessionService.GetSessionData();
            var client = _clientFactory.CreateClient();
            
            client.BaseAddress = new Uri(SpotifyConstants.BaseUri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await client.GetAsync("me");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProfileResponse>(content);
        }

        public async Task<List<Items>?> SearchSongsAsync(MetadataModel songObject)
        {
            var searchQuery = this.BuildSearchQuery(songObject);
            var token = this._sessionService.GetSessionData();
            var client = this._clientFactory.CreateClient();

            client.BaseAddress = new Uri(SpotifyConstants.BaseUri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await client.GetAsync($"search?q={searchQuery}&type=track&limit=1");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(content);

            if (searchResponse == null)
            {
                return default;
            }
            
            return searchResponse.tracks.Items.ToList();
        }

        public async Task<PlaylistCreatedResponse?> CreatePlaylist(CreatePlaylistRequestModel model)
        {
            var token = _sessionService.GetSessionData();
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var userId = "3zwfzfzi022aum4p7ri3qe9n0";
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{SpotifyConstants.BaseUri}users/{userId}/playlists", stringContent);
            
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PlaylistCreatedResponse>(content);

        }

        public async Task<List<UserPlaylistDto>?> GetUserPlaylists()
        {
            var token = _sessionService.GetSessionData();
            var client = _clientFactory.CreateClient();
            
            client.BaseAddress = new Uri(SpotifyConstants.BaseUri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            
            var response = await client.GetAsync(SpotifyConstants.UserPlaylistEndpoint);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            var playlistResponse =  JsonConvert.DeserializeObject<UserPlaylistResponse>(content);

            if (playlistResponse == null)
            {
                return null;
            }
            
            return _mapper.Map<List<UserPlaylistDto>>(playlistResponse.Items);
        }

        public async Task<object?> AddToPlaylist(AddToPlaylistDto model)
        {
            if (model.Uris == null || model.PlaylistId == null)
            {
                return null;
            }
            
            var token = _sessionService.GetSessionData();
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(model.Uris), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{SpotifyConstants.BaseUri}playlists/{model.PlaylistId}/tracks", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public bool IsTokenExpired()
        {
            var token = this._sessionService.GetSessionData();
            var expires = DateTime.Parse(token.ExpiresAt);

            return expires >= DateTime.Now;
        }
        
        private string BuildSearchQuery(MetadataModel model)
        {
            var title = model.Title != null ? $"track:{model.Title.Replace(" ", "%20")}" : string.Empty;
            var album = model.Album != null ? $"album:{model.Album.Replace(" ", "%20")}" : string.Empty;
            var artist = model.Artist != null ? $"artist:{string.Join("%20", model.Artist).Replace(" ", "%20")}" : string.Empty;
            
            return $"{title}%20{album}%20{artist}";
        }
    }
}

