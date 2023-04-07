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
        public async Task<ActionResult<string>> GetArtist(string input)
        {
           var a = _artistService.SearchArtist("Slayer", "f9968a478d0249bc820ba9635b7efc70");
            return await a;
        }
    }
}
