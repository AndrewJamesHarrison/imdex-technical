using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imdex.Models;
using Microsoft.AspNetCore.Mvc;

namespace Imdex.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        [HttpGet("sample")]
        public List<DrillHole> getSampleData()
        {
            var drillholes = new List<DrillHole>()
            {
                new DrillHole()
                {
                    Azimuth = 2,
                    Dip = 1.5,
                    Latitude = 65.5,
                    Longitude = 125.0,
                    Readings = new List<DepthReading>()
                    {
                        new DepthReading()
                        {
                            Azimuth = 1.5,
                            Dip = 0.7,
                            Depth = 100,
                        }
                    }
                }
            };
            return drillholes;
        }
    }
}
