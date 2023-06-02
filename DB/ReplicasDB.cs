using ModelClass;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DB
{
    public class ReplicasDB: BaseDB<replicas>
    {
        protected override List<replicas> CreateListModel(List<object[]> rows)
        {
            List<replicas> ReplicasList = new List<replicas>();
            foreach (object[] item in rows)
            {
                replicas c = new replicas();
                c = (replicas)CreateModel(item);
                ReplicasList.Add(c);
            }
            return ReplicasList;

        }
        protected override replicas CreateModel(object[] row)
        {
            replicas c = new replicas();
            c.replicaID = int.Parse(row[0].ToString());
            c.nameartist = row[1].ToString();
            c.namedrawing = row[2].ToString();
            c.location = row[3].ToString();
            c.drawingID =(drawing)row[4];
            c.artist = (artist)row[5];
            
            return c;

        }
        protected override async Task<List<replicas>> CreateListModelAsync(List<object[]> rows)
        {
            List<replicas> replicasList = new List<replicas>();
            foreach (object[] item in rows)
            {
                replicas o = new replicas();
                o = (replicas)await CreateModelAsync(item);
                replicasList.Add(o);
            }
            return replicasList;

        }
        protected override async Task<replicas> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT replicas.* FROM replicas WHERE (replicaID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<replicas> list = (List<replicas>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        protected override async Task<replicas> CreateModelAsync(object[] row)
        {
            replicas o = new replicas();
            o.replicaID = int.Parse(row[0].ToString());
            o.nameartist = row[1].ToString();
            o.namedrawing = row[2].ToString();
            int drawingID = int.Parse(row[3].ToString());
            drawing a = new drawing();
            DrawingDB drawingDB = new DrawingDB();
            a = await drawingDB.SelectByPkAsync(drawingID);
            o.drawingID = a;
            return o;

        }
        public override replicas GetRowByPK(object pk)
        {
            string sql = @"SELECT replicas.* FROM replicas WHERE
			 	(replicaID = @id)";
            cmd.Parameters.AddWithValue("@id", int.Parse(pk.ToString()));
            List<replicas> list = (List<replicas>)SelectAll(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;


        }
        protected override string GetTableName()
        {
            return "replicas";
        }

    }
}
