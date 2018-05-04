using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using Newtonsoft.Json.Linq;
using Process;
namespace dotnetItinero
{
   
    class Program
    {
        static void Main(string[] args)
        {

            string OSMFilePath =string.Empty;
            float latSource ,longSource,latDest ,longDest;//baseball ground
           
            
            Console.WriteLine("Enter Source Latitude:");
            latSource = getInput();
            Console.WriteLine("Enter Source Longitude:");
            longSource = getInput();
            Console.WriteLine("Enter Destination Latitude:");
            latDest = getInput();
            Console.WriteLine("Enter Destination Longitude:");
            longDest = getInput();

            RoutePlanner rp = new RoutePlanner(latSource, longSource, latDest, longDest, OSMFilePath);

            List<Waypoints> wp = rp.getRoute();
           
            foreach (Waypoints w in wp)
            {
                //Console.WriteLine("----------------------------------------------------------/n");
                //Console.WriteLine("Latitude:{0},Longitude:{1}", w.lat, w.longi);
                Console.WriteLine("[{0},{1}]", w.Lat, w.Longi);
            }
            Console.Read();
        }

        private static float getInput()
        {
            float f_input=0;
            string input = Console.ReadLine();
            try
            {
                input = input != null && input != string.Empty && float.TryParse(input, out f_input) ? input : "0";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return f_input;
        }
    }
}
