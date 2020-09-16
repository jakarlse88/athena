using System;
using System.Threading.Tasks;
using Athena.Services;
using Athena.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> Post(TechniqueViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}