using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;
using Org.BouncyCastle.Asn1.X509;

namespace DB
{
    public class ArtisiDB : BaseDB<artist>
    {
        protected override List<artist> CreateListModel(List<object[]> rows)
        {
            List<artist> artList = new List<artist>();
            foreach (object[] item in rows)
            {
                artist c = new artist();
                c = (artist)CreateModel(item);
                artList.Add(c);
            }
            return artList;

        }
        protected override async Task<artist> CreateModelAsync(object[] row)
        {
            artist o = new artist();
            o.artistID = int.Parse(row[0].ToString());
            o.Name = row[1].ToString();
            o.Birthday = row[2].ToString();
            o.Password = row[3].ToString();
            return o;
        }
        protected override async Task<List<artist>> CreateListModelAsync(List<object[]> rows)
        {
            List<artist> custList = new List<artist>();
            foreach (object[] item in rows)
            {
                artist c = new artist();
                c = (artist)CreateModel(item);
                custList.Add(c);
            }
            return custList;
        }
        protected override async Task<artist> GetRowByPKAsync(object pk)
        {
            string sql = @"SELECT artist.* FROM artist WHERE
			 	(artistID = @id)";
            cmd.Parameters.AddWithValue("@id", int.Parse(pk.ToString()));
            List<artist> list = (List<artist>)SelectAll(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }
        protected override artist CreateModel(object[] row)
        {
            artist c = new artist();
            c.artistID = int.Parse(row[2].ToString());
            c.Name = row[0].ToString();
            c.Birthday = row[1].ToString();
            c.Password = row[2].ToString();
            return c;
        }
        public override artist GetRowByPK(object pk)
        {
            string sql = @"SELECT artist.* FROM artist WHERE
			 	(artistID = @id)";
            cmd.Parameters.AddWithValue("@id", int.Parse(pk.ToString()));
            List<artist> list = (List<artist>)SelectAll(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;


        }
        protected override string GetTableName()
        {
            return "artist";
        }
        public void Insert(artist at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"name",at.Name},
                            {"Birthday",at.Birthday} };
            base.Insert(d);
        }
        public int Update(artist at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>(){
                            {"artistID",at.artistID.ToString()},
                            {"name",at.Name},
                            {"Birthday",at.Birthday} };
            Dictionary<string, string> d2 = new Dictionary<string, string>()
            {
                {"artistID",at.artistID.ToString()} };
           
            return base.Update(d,d2);
        }
        public async Task<int> UpdateAsync(artist a)
        {
            Dictionary<string, string> fillValues = new Dictionary<string, string>();
            Dictionary<string, string> filterValues = new Dictionary<string, string>();
            fillValues.Add("Name", a.Name);
            fillValues.Add("Birthday", a.Birthday);
            filterValues.Add("artistID", a.artistID.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }
        public int Delete(artist at)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("artistID",at.artistID.ToString());
            return base.Delete(d);
        }
        public async Task<int> DeleteAsync(artist a)
        {
            Dictionary<string, string> filterValues = new Dictionary<string, string>
            {
                { "CustomerID", a.artistID.ToString() }
            };
            return await base.DeleteAsync(filterValues);
        }
        public int updatePassword(artist a, string password)
        {
            Dictionary<string, string> fillValues = new Dictionary<string, string>();
            Dictionary<string, string> filterValues = new Dictionary<string, string>();
            fillValues.Add("Name", a.Name);
            fillValues.Add("Birthday", a.Birthday);
            fillValues.Add("ArtistPassword", password);
            filterValues.Add("ArtistID", a.artistID.ToString());
            return base.Update(fillValues, filterValues);
        }
        public async Task<int> updatePasswordAsync(artist a, string password)
        {
            Dictionary<string, string> fillValues = new Dictionary<string, string>();
            Dictionary<string, string> filterValues = new Dictionary<string, string>();
            fillValues.Add("Name", a.Name);
            fillValues.Add("Birthday", a.Birthday);
            fillValues.Add("CustomerPassword", password);
            filterValues.Add("CustomerID", a.artistID.ToString());
            return await base.UpdateAsync(fillValues, filterValues);
        }
      
       
        /// <summary>
        /// insert new records in a table using INSERT Statement.
        /// </summary>
        /// <param name="keyAndValue">Dictionary (Key & Value)</param>
        /// <returns>An object that includes the ID attribute from the database.</returns>
        public object InsertGetObj(Dictionary<string, string> keyAndValue)
        {
            string sqlCommand = PrepareInsertQueryWithParameters(keyAndValue);
            if (sqlCommand != "")
            {
                sqlCommand += $" SELECT LAST_INSERT_ID();";
                object res = ExecScalar(sqlCommand);
                if (res != null)
                {
                    return GetRowByPK(res);
                }
            }
            return null;
        }
        public async Task<artist> InsertGetObjAsync(artist a, string password)
        {
            Dictionary<string, string> fillValues = new Dictionary<string, string>()
            {
                { "Name", a.Name },
                { "Email", a.Birthday },
                { "CustomerPassword", password }
            };
            return (artist)await base.InsertGetObjAsync(fillValues);
        }
        /// <summary>
        /// Prepare Insert Query With Parameters
        /// </summary>
        /// <param name="fields">Dictionary (Key & Value)</param>
        /// <returns>String of SQL</returns>
        private string PrepareInsertQueryWithParameters(Dictionary<string, string> fields)
        {
            if (fields == null || fields.Count == 0)
                return "";

            string InKey = "(" + string.Join(",", fields.Keys) + ")";
            string InValue = "VALUES(";
            for (int i = 0; i < fields.Values.Count; i++)
            {
                string pn = "@" + i;
                InValue += pn + ',';
                AddParameterToCommand(pn, fields.Values.ElementAt(i));
            }
            InValue = InValue.Remove(InValue.Length - 1);//remove last ,
            InValue += ")";

            string sqlCommand = $"INSERT INTO {GetTableName()}  {InKey} {InValue};";
            return sqlCommand;
        }
        /// <summary>
        /// TESTED asynchronous version of SelectByPk
        /// get one customer from Database by primary key 
        /// </summary>
        /// <param name="id">SQL string</param>
        /// <returns>One object of type customer.</returns>
        public async Task<artist> SelectByPkAsync(int id)
        {
            string sql = @"SELECT artist.* FROM artist WHERE (artistID = @id)";
            AddParameterToCommand("@id", id);
            List<artist> list = (List<artist>)await SelectAllAsync(sql);
            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

     
    }
}
