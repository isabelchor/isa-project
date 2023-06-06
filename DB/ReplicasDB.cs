using ModelClass;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DB
{
    public class ReplicasDB : BaseDB<replicas>
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
            c.replicaID = int.Parse(row[1].ToString());
            c.location = row[0].ToString();
            c.drawingID = int.Parse(row[2].ToString());

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
        public override async Task<replicas> GetRowByPKAsync(object pk)
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
            replicas c = new replicas();
            c.replicaID = int.Parse(row[1].ToString());
            c.location = row[0].ToString();
            c.drawingID = int.Parse(row[2].ToString());

            return c;

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

        public bool Delete(replicas re)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            { "replicaID", re.replicaID.ToString()} };
            return base.Delete(d) != -1;
        }
        public bool Insert(replicas at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                  {"drawingID",at.drawingID.ToString()},
                  { "replicaID", at.replicaID.ToString()},
                            {"location",at.location} };

            return base.Insert(d)!=-1;
        }
        public bool Update(replicas at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                             {"location",at.location} };

            Dictionary<string, string> d2 = new Dictionary<string, string>(){
                     { "replicaID",at.replicaID.ToString()} };

            return base.Update(d, d2) != -1;
        }
    }

}

