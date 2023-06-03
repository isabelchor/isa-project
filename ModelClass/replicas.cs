using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class replicas
    {

        public string location { get; set; }
        public int replicaID { get; set; }
        public int drawingID { get; set; }
        public replicas( string location, int replicaID, int drawingID)
        {
            this.location = location;
            this.replicaID = replicaID;
            this.drawingID = drawingID;
        }
        public replicas() { }
    }
}
