using dotnet_API.Extensions;
using dotnet_API.Interfaces;
using dotnet_API.Models;
using dotnet_API.Repositories;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository _artistRepository;
        private readonly IArtistService _artistService;
        public ArtistController(ArtistRepository artistRepository, IArtistService artistService)
        {
            _artistRepository = artistRepository;
            _artistService = artistService;
        }

        [HttpPost("/GetArtist")]
        public async Task<ActionResult<JObject>> GetArtist(string input)
        {
            var artistName = input.ToLower().RemoveDiacritics();

            var weHaveThisArtist = _artistRepository.GetAll()
                .Any(x => x.Name == artistName);

            if (weHaveThisArtist)
                return Ok();

            var flurlClient = new FlurlClient();
            var response = await flurlClient.Request($"https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={input}&api_key=cd8ca4bac789ae2dae3dfeb568bf2df5&format=json").GetJsonAsync<JObject>();

            return response;   
        }
    }
}
