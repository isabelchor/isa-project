using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;

namespace DB
{
    public class TypeDB : BaseDB<type>
    {
        protected override type CreateModel(object[] row)
        {
            type c = new type();
            c.ID = int.Parse(row[0].ToString());
            c.types = row[1].ToString();
            return c;
        }
        protected override async Task<type> CreateModelAsync(object[] row)
        {
            type c = new type();
            c.ID = int.Parse(row[0].ToString());
            c.types = row[1].ToString();
            return c;
        }
        public override type GetRowByPK(object pk)
        {
            string sql = $"SELECT type.* FROM type WHERE (ID = @id)";
            cmd.Parameters.AddWithValue("@id", int.Parse(pk.ToString()));
            List<type> list = (List<type>)SelectAll(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        public override async Task<type> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT type.* FROM type WHERE
			 	(ID = @id)";
            AddParameterToCommand("@id", int.Parse(pk.ToString()));
            List<type> list = (List<type>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        protected override List<type> CreateListModel(List<object[]> rows)
        {
            List<type> custList = new List<type>();
            foreach (object[] item in rows)
            {
                type c;
                c = CreateModel(item);
                custList.Add(c);
            }
            return custList;
        }
        protected override async Task<List<type>> CreateListModelAsync(List<object[]> rows)
        {
            List<type> custList = new List<type>();
            foreach (object[] item in rows)
            {
                type c = new type();
                c = (type)CreateModel(item);
                custList.Add(c);
            }
            return custList;
        }
        protected override string GetTableName()
        {
            return "type";
        }
        public void Insert(type at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"types",at.types} };
            base.Insert(d);
        }
        public void Update(type at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"ID",at.ID.ToString()},
                            {"types",at.types} };
            Dictionary<string, string> d2 = new Dictionary<string, string>()
            {
                {"ID",at.ID.ToString()} };

            base.Update(d, d2);
        }
        public void Delete(type at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            { "ID" , at.ID.ToString()},
                            { "types",at.types} };
            base.Delete(d);
        }


    }
}
