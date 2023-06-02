using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    public class drawing
    {
        public int TypeID { get; set; }
        public string date { get; set; }
        public string namedrawing { get; set; }
        public int cost { get; set; }
        public string technique { get; set; }
        public int artistID { get; set; }
        public int drawingID { get; set; }
        public drawing(int type, string date, string namedrawing, string name, int cost, string technique, int artist, int drawingID)
        {
            this.TypeID = type;
            this.date = date;
            this.namedrawing = namedrawing;
            this.cost = cost;
            this.technique = technique;
            this.artistID = artist;
            this.drawingID = drawingID;
        }
       public drawing() { }
    }
}
