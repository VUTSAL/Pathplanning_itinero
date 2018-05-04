using System;
using System.Collections.Generic;
using System.IO;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using Newtonsoft.Json.Linq;

namespace Process
{
    public class RoutePlanner
    {
        #region DATAMEMBERS
        List<Waypoints> wp = new List<Waypoints>();
        float latStart = (float)34.057693;
        float longStart = (float)-117.826707;//healthcenter

        float latEnd = (float)34.053849;
        float longEnd = (float)-117.815225;//baseball ground
       
        private string filePath = "C:\\Users\\VUTSAL\\Desktop\\FOLDER\\GolfCartProject\\Project\\dotnetItinero\\california-latest.osm.pbf";
        public string OSMPath { get { return filePath; } set { filePath = value; } }
        #endregion
        #region Constructors

        public RoutePlanner()
        {

        }
        public RoutePlanner(float SourceLat, float SourceLong, float DestLat, float DestLong,string path)
        {
            this.latStart = SourceLat == 0 ? this.latStart:SourceLat ;
            this.longStart = SourceLong == 0 ? this.longStart : SourceLong;

            this.latEnd = DestLat == 0 ? this.latEnd : DestLat;
            this.longEnd = DestLong == 0 ? this.longEnd : DestLong;
            OSMPath = path == string.Empty ? OSMPath : path;
        } 
        
        #endregion

        #region Methods
        public List<Waypoints> getRoute()
        {

            try
            {
                JObject parsed = calculateRoute();

                createWaypointList(parsed);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return wp;
        }


        private JObject calculateRoute()
        {
            try
            {
                var routerDb = new RouterDb();
                using (var stream = new FileInfo(filePath).OpenRead())
                {
                    Console.WriteLine("Loading File");
                    routerDb.LoadOsmData(stream, Vehicle.Pedestrian);
                    Console.WriteLine("File Loaded");
                }
                var router = new Router(routerDb);

                // get a profile.
                var profile = Vehicle.Pedestrian.Shortest(); // the default OSM car profile.
                                                             // calculate a route.
                Console.WriteLine("Calculating Route");

                var start = router.Resolve(profile, latStart, longStart, 30);
                var end = router.Resolve(profile, latEnd, longEnd, 30);
                var route = router.Calculate(profile, start, end);

                var geoJson = route.ToGeoJson();



                JObject parsed = JObject.Parse(geoJson);
                return parsed;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        private void createWaypointList(JObject parsed)
        {
            try
            {
                Console.WriteLine("Printing route");
                //Console.WriteLine(geoJson);
                foreach (var pair in parsed["features"])
                {
                    string lat = string.Empty;
                    int cont = 0;
                    foreach (var coord in pair["geometry"]["coordinates"])
                    {

                        if (coord.HasValues)
                        {

                            Waypoints w = new Waypoints(coord[0].ToString(), coord[1].ToString());
                            wp.Add(w);
                        }
                        else
                        {
                            //Console.WriteLine(coord);
                            lat = lat == string.Empty ? coord.ToString() : lat;
                            if (cont == 1)
                            {

                                Waypoints w = new Waypoints(lat, coord.ToString());
                                wp.Add(w);
                            }
                        }

                        cont++;

                    }



                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
