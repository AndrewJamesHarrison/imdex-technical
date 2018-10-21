using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imdex.Models;
using Imdex.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Imdex.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly IDepthReadingRepository _depthReadingRepository;
        private readonly IDrillHoleRepository _drillHoleRepository;

        public DataController(IDrillHoleRepository drillHoleRepository, IDepthReadingRepository depthReadingRepository)
        {
            _depthReadingRepository = depthReadingRepository;
            _drillHoleRepository = drillHoleRepository;
        }

        [HttpGet("sample")]
        public List<DrillHole> getSampleData()
        {
            var drillholes = _drillHoleRepository.GetAll();
            if(drillholes.Count() > 0)
            {
                foreach(DrillHole hole in drillholes)
                {
                    if (hole.Readings.Count() > 0)
                    {
                        if (hole.Readings.First() != null)
                        {
                            hole.Readings.Sort((x, y) => x.Depth.CompareTo(y.Depth));
                            hole.Readings.First().SetTrust(hole.Azimuth, hole.Dip);
                            for (int i = 1; i < hole.Readings.Count(); i++)
                            {
                                if (hole.Readings[i] != null)
                                {
                                    hole.Readings[i].SetTrust(hole.Readings[i - 1].Azimuth, hole.Readings.SkipLast(hole.Readings.Count() - i).TakeLast(5).Average(r => r.Dip));
                                }
                            }
                        }
                    }
                }
            }
            return drillholes;
        }


    }
}
