using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PyramidWord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PyramidController : Controller
    {
        private readonly ILogger<PyramidController> _logger;

        public PyramidController(ILogger<PyramidController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("PyramidWordFinder")]
        public bool PyramidWordFinder(string strWord)
        {
            try
            {
                if (!string.IsNullOrEmpty(strWord))
                {
                    var letterGroup = strWord.GroupBy(i => i.ToString(), StringComparer.OrdinalIgnoreCase)
                                             .Select(i => i.Count())
                                             .OrderBy(i => i);

                    return letterGroup.SequenceEqual(Enumerable.Range(1, letterGroup.ToList().Count()));
                }

                else
                    return false;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while checking for a pyramid word '{strWord}'", null);
                return false;
            }
        }
    }
}
