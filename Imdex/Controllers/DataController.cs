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
            return drillholes;
        }
    }
}
