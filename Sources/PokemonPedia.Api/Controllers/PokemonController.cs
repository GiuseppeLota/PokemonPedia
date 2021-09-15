using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPedia.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        [HttpGet("{name}")]
        public string Translate(string name)
        {
            return name;
        }
    }
}
