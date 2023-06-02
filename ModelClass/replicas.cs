using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class replicas
    {

        public string namedrawing { get; set; }
        public string location { get; set; }
        public string nameartist { get; set; }
        public int replicaID { get; set; }
        public artist artist { get; set; }
        public drawing drawingID { get; set; }
        public replicas(string namedrawing, string location, string nameartist, int replicaID, artist artist, drawing drawingID)
        {
            this.namedrawing = namedrawing;
            this.location = location;
            this.nameartist = nameartist;
            this.replicaID = replicaID;
            this.artist = artist;
            this.drawingID = drawingID;
        }
        public replicas() { }
    }
}
