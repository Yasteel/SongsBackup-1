namespace SongsBackup.Services
{
    using System.Text;
    using System.Net.Http.Headers;

    using Newtonsoft.Json;

    using SongsBackup.Interfaces;
    using SongsBackup.Models;
    using SongsBackup.Models.SpotifyModels;
    using SongsBackup.Models.SpotifyModels.RequestModel;

    public class SpotifyService : ISpotifyService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ISessionService sessionService;

        public SpotifyService(IHttpClientFactory clientFactory, ISessionService sessionService)
        {
            this.clientFactory = clientFactory;
            this.sessionService = sessionService;
        }
        
        public async Task<ProfileResponse?> GetProfile()
        {
            var token = this.sessionService.GetSessionData();
            var client = this.clientFactory.CreateClient();
            
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

        public async Task<string?> SearchSongsAsync(MetadataModel songObject)
        {
            var searchQuery = this.BuildSearchQuery(songObject);
            var token = this.sessionService.GetSessionData();
            var client = this.clientFactory.CreateClient();

            client.BaseAddress = new Uri(SpotifyConstants.BaseUri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await client.GetAsync($"search?q={searchQuery}&type=track&limit=1");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return response.StatusCode.ToString();
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SearchResponse>(content);
            return content;
        }

        public async Task<PlaylistCreatedResponse?> CreatePlaylist(CreatePlaylistRequestModel model)
        {
            var token = this.sessionService.GetSessionData();
            var client = this.clientFactory.CreateClient();
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

        public async Task<UserPlaylistResponse?> GetUserPlaylists()
        {
            var token = this.sessionService.GetSessionData();
            var client = this.clientFactory.CreateClient();
            
            client.BaseAddress = new Uri(SpotifyConstants.BaseUri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            
            var response = await client.GetAsync(SpotifyConstants.UserPlaylistEndpoint);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserPlaylistResponse>(content);
        }

        public async Task<object?> AddToPlaylist(string playlistId, AddToPlaylistRequestModel songs)
        {
            var token = this.sessionService.GetSessionData();
            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(songs), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{SpotifyConstants.BaseUri}playlists/{playlistId}/tracks", stringContent);

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
            var token = this.sessionService.GetSessionData();
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

