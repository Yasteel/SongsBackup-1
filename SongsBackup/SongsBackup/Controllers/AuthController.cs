namespace SongsBackup.Controllers
{
    using System.Net.Http.Headers;
    using System.Text;
    
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.AspNetCore.Mvc;
        
    using Newtonsoft.Json;
    
    using Interfaces;
    using Models.SpotifyModels;
    
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionService _sessionService;

        public AuthController(IHttpClientFactory clientFactory, ISessionService sessionService)
        {
            this._clientFactory = clientFactory;
            this._sessionService = sessionService;
        }
        
        // GET
        [HttpGet("login")]
        public IActionResult Login()
        {
            var clientId = SpotifyConstants.ClientId;
            var redirectUri = "https://localhost:44372/auth/callback";
            var state = this.GenerateRandomString(10);
            var scope = "user-read-private user-read-email playlist-modify-private playlist-modify-public";

            var reqParams = new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["response_type"] = "code",
                ["scope"] = scope,
                ["redirect_uri"] = redirectUri,
                ["state"] = state,
                ["show_dialog"] = true.ToString()
            };
            // Was in Request Parameters

            var queryParams = new Uri(QueryHelpers.AddQueryString(SpotifyConstants.AuthEndpoint, reqParams!));

            return this.Redirect(queryParams.ToString());
        }
        
        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery]string code, [FromQuery]string state)
        {
            try
            {
                var client = this._clientFactory.CreateClient();
                var clientId = SpotifyConstants.ClientId;
                var clientSecret = SpotifyConstants.ClientSecret;
                var redirectUri = "https://localhost:44372/auth/callback";

                var tokenEndpoint = "https://accounts.spotify.com/api/token";

                var tokenRequest = new Dictionary<string, string>
                {
                    { "code", code },
                    { "redirect_uri", redirectUri },
                    { "grant_type", "authorization_code" }
                };

                var tokenRequestBody = new FormUrlEncodedContent(tokenRequest);
                var clientCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientCredentials);

                var tokenResponse = await client.PostAsync(tokenEndpoint, tokenRequestBody);

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    return BadRequest(); // Handle the error accordingly
                }

                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<SpotifyTokenResponse>(tokenContent);
                if (token != null) this._sessionService.SetSessionData(token);


                // var test = this._sessionService.GetSessionData();

                return this.RedirectToAction("Index", "Landing");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        private string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}