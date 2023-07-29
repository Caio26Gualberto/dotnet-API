using dotnet_API.Interfaces;
using dotnet_API.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<string>> GetArtist(string artistName)
        {
           var artist = _artistService.SearchArtist(artistName);
            return await artist;
        }
    }
}
