using System;
using System.Collections.Generic;
using System.Text;

namespace Process
{
    public class Waypoints
    {
        private string lat;
        private string longi;
        public string Lat { get { return lat; } set { lat = value; } }
        public string Longi { get { return longi; } set { longi = value; } }
        public Waypoints()
        {

        }
        public Waypoints(string LAT, string LONG)
        {
            this.Lat = LAT;
            this.Longi = LONG;
        }
    }
}
