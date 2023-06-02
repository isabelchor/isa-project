using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public abstract class DB
    {
        protected const string connSTR = @"server=localhost;
                                    user id=root;
                                    password=josh17rog;
                                    persistsecurityinfo=True;
                                    database=exhibition";
        protected static MySqlConnection conn;
        protected MySqlCommand cmd;
        protected DbDataReader reader;

        protected DB()
        {
            conn = new MySqlConnection(connSTR);
            cmd = new MySqlCommand();
            cmd.Connection = conn;
        }
    }
}
