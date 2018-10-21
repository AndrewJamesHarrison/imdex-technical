using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imdex.Models
{
    public class DepthReading
    {
        public int DepthId { get; }
        public double Depth { get; set; }
        public double Dip { get; set; }
        public double Azimuth { get; set; }

        public bool Trustworthy { get; set; }

        public void SetTrust(double prevAzimuth, double prevDip)
        {
            var diffAzimuth = Azimuth - prevAzimuth;
            var diffDip = Dip - prevDip;

            Trustworthy = (diffAzimuth < 5 && diffAzimuth > -5 && diffDip < 3 && diffDip > -3);
        }
    }
}
