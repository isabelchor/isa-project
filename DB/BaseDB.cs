using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public abstract class BaseDB<T> : DB
    {
        protected abstract string GetTableName();
        public abstract T GetRowByPK(object pk);
        protected abstract Task<T> GetRowByPKAsync(object pk);
        protected abstract T CreateModel(object[] row);
        protected abstract Task<T> CreateModelAsync(object[] row);
        protected abstract List<T> CreateListModel(List<object[]> rows);
        protected abstract Task<List<T>> CreateListModelAsync(List<object[]> rows);
        protected async Task<object> InsertGetObjAsync(Dictionary<string, string> keyAndValue)
        {
            string sqlCommand = PrepareInsertQueryWithParameters(keyAndValue);
            sqlCommand += $" SELECT LAST_INSERT_ID();";
            object res = await ExecScalarAsync(sqlCommand);
            if (res != null)
            {
                return GetRowByPK(res);
            }
            else
                return null;
        }
        protected object InsertGetObj(Dictionary<string, string> keyAndValue)
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
        public object SelectAll()
        {
            List<object[]> list = (List<object[]>)StingListSelectAll("", new Dictionary<string, string>());
            return CreateListModel(list);
        }
        public object SelectAll(Dictionary<string, string> parameters)
        {
            List<object[]> list = (List<object[]>)StingListSelectAll("", parameters);
            return CreateListModel(list);
        }
        public object SelectAll(string query)
        {
            List<object[]> list = (List<object[]>)StingListSelectAll(query, new Dictionary<string, string>());
            return CreateListModel(list);
        }
        public object SelectAll(string query, Dictionary<string, string> parameters)
        {
            List<object[]> list = (List<object[]>)StingListSelectAll(query, parameters);
            return CreateListModel(list);
        }
        protected object StingListSelectAll(string query, Dictionary<string, string> parameters)
        {
            object list = new List<object[]>();
            string where = "WHERE ";
            if (parameters != null && parameters.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    where += $"{param.Key} = {param.Value}";
                    i++;
                    if (i < parameters.Count)
                        where += " AND ";
                }
            }
            else
                where = "";
            string sqlCommand = $"{query} {where}";
            if (String.IsNullOrEmpty(query))
                sqlCommand = $"SELECT * FROM {GetTableName()} {where}";
            base.cmd.CommandText = sqlCommand;
            if (DB.conn.State != System.Data.ConnectionState.Open)
                DB.conn.Open();
            if (base.cmd.Connection.State != System.Data.ConnectionState.Open)
                base.cmd.Connection = DB.conn;

            try
            {
                this.reader = base.cmd.ExecuteReader();
                int size = reader.GetColumnSchema().ToArray().Length;
                object[] row;
                while (this.reader.Read())
                {
                    row = new object[size];
                    this.reader.GetValues(row);
                    ((List<object[]>)list).Add(row);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText);
                ((List<string[]>)list).Clear();
            }
            finally
            {
                base.cmd.Parameters.Clear();
                if (reader != null) reader.Close();
                if (DB.conn.State == System.Data.ConnectionState.Open)
                    DB.conn.Close();
            }
            return list;
        }
        protected int exeNONquery(string query)
        {
            cmd.CommandText = query;
            cmd.Connection = DB.conn;
            if(DB.conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            int num = cmd.ExecuteNonQuery();
            conn.Close();
            return num;
        }
        protected int Insert(Dictionary<string, string> FildValue)
        {

            //  INSERT INTO table_name(column1, column2, column3, ...)
            //  VALUES(value1, value2, value3, ...);

            string sql = "INSERT INTO " + GetTableName() + " (";
            int i = 0;
            foreach (KeyValuePair<string, string> param in FildValue)
            {
                i++;
                if (i < FildValue.Count)
                    sql += param.Key + ", ";
                else
                    sql += param.Key + ") ";
            }
            sql += " VALUES('";
            i = 0;
            foreach (KeyValuePair<string, string> param in FildValue)
            {
                i++;
                if (i < FildValue.Count)
                    sql += param.Value + "',' ";
                else
                    sql += param.Value + "') ";
            }
            sql += ";";
            base.cmd.CommandText = sql;
            DB.conn.Open();
            int num = base.cmd.ExecuteNonQuery();
            return num;
        }
        protected int Update(Dictionary<string, string> FildValue, Dictionary<string, string> parameters)
        {
            //UPDATE table_name
            //SET column1 = value1, column2 = value2, ...
            //WHERE condition;

            string query = $"UPDATE {GetTableName()} ";
            string val = "SET ";
            if (FildValue != null && FildValue.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> param in FildValue)
                {
                    val += $"{param.Key} = '{param.Value}'";
                    i++;
                    if (i < FildValue.Count)
                        val += ", ";
                }
            }
            string where = "WHERE ";
            if (parameters != null && parameters.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    where += $"{param.Key} = '{param.Value}'";
                    i++;
                    if (i < parameters.Count)
                        where += " AND ";
                    else
                        where += ";";
                }
            }
            else
                where = "";
            query += val + " " + where;
            return exeNONquery(query);

        }
        protected int Delete(Dictionary<string, string> parameters)
        {
            string query = $"DELETE FROM {GetTableName()} ";
            string where = "WHERE ";
            if (parameters != null && parameters.Count > 0)
            {
                int i = 0;
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    where += $"{param.Key} = {param.Value}";
                    i++;
                    if (i < parameters.Count)
                        where += " AND ";
                    else
                        where += ";";
                }
            }
            else
                where = "";
            query += where;
            return exeNONquery(query);
        }

        private void PreQuery(string query)
        {
            cmd.CommandText = query;
            if (DB.conn.State != System.Data.ConnectionState.Open)
                DB.conn.Open();
            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                cmd.Connection = DB.conn;
        }


        private void PostQuery()
        {
            if (reader != null && !reader.IsClosed)
                reader?.Close();

            cmd.Parameters.Clear();
            if (DB.conn.State == System.Data.ConnectionState.Open)
                DB.conn.Close();
        }
        protected object ExecScalar(string query)
        {
            if (String.IsNullOrEmpty(query))
                return null;

            PreQuery(query);
            object obj = null;
            try
            {
                obj = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText);
            }
            finally
            {
                PostQuery();
            }
            return obj;
        }

        protected void AddParameterToCommand(string name, object value)
        {
            DbParameter p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    
        public async Task<List<T>> SelectAllAsync()
        { 
            return await SelectAllAsync("", new Dictionary<string, string>());
        }


        public async Task<List<T>> SelectAllAsync(Dictionary<string, string> parameters)
        {
            return await SelectAllAsync("", parameters);
        }


        public async Task<List<T>> SelectAllAsync(string query)
        {
            return await SelectAllAsync(query, new Dictionary<string, string>());
        }


        public async Task<List<T>> SelectAllAsync(string query, Dictionary<string, string> parameters)
        {
            List<object[]> list = await StingListSelectAllAsync(query, parameters);
            return CreateListModel(list);
        }


        protected async Task<int> ExecNonQueryAsync(string query)
        {
            if (String.IsNullOrEmpty(query))
                return 0;
            PreQuery(query);
            int rowsEffected = 0;
            try
            {
                rowsEffected = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText);
            }
            finally
            {
                PostQuery();
            }
            return rowsEffected;
        }


        protected async Task<object> ExecScalarAsync(string query)
        {
            if (String.IsNullOrEmpty(query))
                return null;
            PreQuery(query);
            object obj = null;
            try
            {
                obj = await cmd.ExecuteScalarAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText);
            }
            finally
            {
                PostQuery();
            }
            return obj;
        }

        /// <summary>
        /// asynchronous version of Insert
        /// insert new records in a table using INSERT Statement.
        /// </summary>
        /// <param name="keyAndValue">Dictionary (Key & Value)</param>
        /// <returns>The number of rows affected.</returns>
        protected async Task<int> InsertAsync(Dictionary<string, string> keyAndValue)
        {
            string sqlCommand = PrepareInsertQueryWithParameters(keyAndValue);
            return await ExecNonQueryAsync(sqlCommand);
        }


        private string PrepareUpdateQueryWithParameters(Dictionary<string, string> fields)
        {
            string InValue = "";
            if (fields != null && fields.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in fields)
                {
                    string prm = $"@{param.Key}";
                    InValue += $"{param.Key}={prm},";
                    AddParameterToCommand(prm, param.Value);
                }
                InValue = InValue.Remove(InValue.Length - 1); //remove last ,
            }
            return InValue;
        }

        protected async Task<int> UpdateAsync(Dictionary<string, string> FildValue, Dictionary<string, string> parameters)
        {
            string where = PrepareWhereQueryWithParameters(parameters);

            string InKeyValue = PrepareUpdateQueryWithParameters(FildValue);
            if (string.IsNullOrEmpty(InKeyValue))
                return 0;

            string sqlCommand = $"UPDATE {GetTableName()} SET {InKeyValue}  {where}";
            return await ExecNonQueryAsync(sqlCommand);
        }

        private string PrepareWhereQueryWithParameters(Dictionary<string, string> parameters)
        {
            string where = "WHERE ";
            string AND = "AND";
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    //where += $"{param.Key} = {param.Value} {AND} ";
                    string prm = $"@W{param.Key}";
                    where += $"{param.Key}={prm} {AND} ";
                    AddParameterToCommand(prm, param.Value);
                }
                where = where.Remove(where.Length - AND.Length - 2);//remove last ' AND '
            }
            else
                where = "";
            return where;
        }

        protected async Task<int> DeleteAsync(Dictionary<string, string> parameters)
        {
            string where = PrepareWhereQueryWithParameters(parameters);

            string sqlCommand = $"DELETE FROM {GetTableName()} {where}";
            return await ExecNonQueryAsync(sqlCommand);
        }
       

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

        

        private async Task<List<object[]>> StingListSelectAllAsync(string query, Dictionary<string, string> parameters)
        {
            List<object[]> list = new List<object[]>();
            string where = PrepareWhereQueryWithParameters(parameters);
            string sqlCommand = $"{query} {where}";
            if (String.IsNullOrEmpty(query))
                sqlCommand = $"SELECT * FROM {GetTableName()} {where}";
            PreQuery(sqlCommand);
            try
            {
                reader = await cmd.ExecuteReaderAsync();
                var readOnlyData = await reader.GetColumnSchemaAsync();
                int size = readOnlyData.Count;
                object[] row;
                while (reader.Read())
                {
                    row = new object[size];
                    reader.GetValues(row);
                    list.Add(row);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nsql:" + cmd.CommandText);
                list.Clear();
            }
            finally
            {
                PostQuery();
            }
            return list;
        }
    }

}
