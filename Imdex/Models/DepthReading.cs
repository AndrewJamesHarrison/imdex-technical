using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdex.Models
{
    public class DepthReading : Model
    {
        public double Depth { get; set; }
        public double Dip { get; set; }
        public double Azimuth { get; set; }
        public bool Trustworthy { get; set; }
    }
}
