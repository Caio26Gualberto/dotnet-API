using dotnet_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistRepository _artistRepository;
        public ArtistController(ArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }


    }
}
