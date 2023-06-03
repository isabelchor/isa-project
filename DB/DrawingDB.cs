using ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;
using Org.BouncyCastle.Asn1.X509;

namespace DB
{
    public class DrawingDB : BaseDB<drawing>
    {
        protected override async Task<drawing> CreateModelAsync(object[] row)
        {
            drawing c = new drawing();
            c.cost = int.Parse(row[0].ToString());
            c.date = row[1].ToString();
            c.technique = row[2].ToString();
            c.namedrawing = row[3].ToString();

            c.drawingID = int.Parse(row[4].ToString());

            c.artistID = int.Parse(row[5].ToString());
            c.TypeID = int.Parse(row[6].ToString());
            return c;
        }
        protected override async Task<List<drawing>> CreateListModelAsync(List<object[]> rows)
        {
            List<drawing> orderList = new List<drawing>();
            foreach (object[] item in rows)
            {
                drawing o = new drawing();
                o = (drawing)await CreateModelAsync(item);
                orderList.Add(o);
            }
            return orderList;
        }
        public override async Task<drawing> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT drawing.* FROM drawing WHERE (drawingID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<drawing> list = (List<drawing>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        protected override List<drawing> CreateListModel(List<object[]> rows)
        {
            List<drawing> DrawList = new List<drawing>();
            foreach (object[] item in rows)
            {
                drawing c = new drawing();
                c = (drawing)CreateModel(item);
                DrawList.Add(c);
            }
            return DrawList;

        }
        protected override drawing CreateModel(object[] row)
        {
            drawing c = new drawing();
            c.cost = int.Parse(row[0].ToString());
            c.date = row[1].ToString();
            c.technique = row[2].ToString();
            c.namedrawing = row[3].ToString();

            c.drawingID = int.Parse(row[4].ToString());

            c.artistID = int.Parse(row[5].ToString());
            c.TypeID = int.Parse(row[6].ToString());
            return c;
        }
        public override drawing GetRowByPK(object pk)
        {
            string sql = @"SELECT drawing.* FROM drawing WHERE
			 	(drawingID = @id)";
            cmd.Parameters.AddWithValue("@id", int.Parse(pk.ToString()));
            List<drawing> list = (List<drawing>)SelectAll(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;


        }
        protected override string GetTableName()
        {
            return "drawing";
        }
        public void Insert(drawing at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"namedrawing",at.namedrawing},
                            {"date",at.date},
                            {"cost",at.cost.ToString()},};
                            
            base.Insert(d);
        }
        public void Update(artist at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"artistID",at.artistID.ToString()},
                            {"name",at.Name},
                            {"birthday",at.Birthday} };
            Dictionary<string, string> d2 = new Dictionary<string, string>()
            {
                {"artistID",at.artistID.ToString()} };

            base.Update(d, d2);
        }
        public void Delete(drawing dr)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            { "drawingID", dr.drawingID.ToString()} };
            base.Delete(d);
        }
        public async Task<drawing> SelectByPkAsync(int id)
        {
            string sql = @"SELECT drawing.* FROM drawing WHERE (drawingID = @id)";
            AddParameterToCommand("@id", id);
            List<drawing> list = (List<drawing>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
    }
}
