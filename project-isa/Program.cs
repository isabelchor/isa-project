using DB;
using ModelClass;

namespace project_isa
{
    internal class Program
    {
        static void Main(string[] args)
        {
     //       //TEST CustomerDB SelectAll
     //       ArtisiDB art = new ArtisiDB();
     //       List<artist> lst = (List<artist>)art.SelectAll();
     //       for (int i = 0; i < lst.Count; i++)
     //       {
     //           Console.WriteLine(@$" id={lst[i].artistID} name={lst[i].Name}
				 //Birthday={lst[i].Birthday}");
     //       }


            ArtisiDB at = new ArtisiDB();
            artist aaa = new artist();
            aaa.Name = "Gadi";
            aaa.Birthday = "28/9/1971";
            at.Insert(aaa);
            

        }
    }
}