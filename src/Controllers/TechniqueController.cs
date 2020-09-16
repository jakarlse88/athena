using Athena.Services;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniqueController
    {
        private readonly ITechniqueService _techniqueService;

        public TechniqueController(ITechniqueService techniqueService)
        {
            _techniqueService = techniqueService;
        }
    }
}