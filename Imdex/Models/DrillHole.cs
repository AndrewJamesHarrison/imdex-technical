using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdex.Models
{
    public class DrillHole
    {
        public int Id { get; }
        public double Latitude { get; set;}
        public double Longitude { get; set; }
        public double Dip { get; set; }
        public double Azimuth { get; set; }

        public List<DepthReading> Readings { get; set; } 
    }
}
