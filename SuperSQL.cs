using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace resolver
{
    public class SuperSQL
    {
        public SuperSQL(SqlConnection con, SqlTransaction trn, HttpResponse rsp = null)
        {
            this.con = con;
            this.trn = trn;
            this.rsp = rsp;
        }

        public void add(String name, object val)
        {
            vals[name] = val;
        }

        public void where(String name, object val)
        {
            wheres[name] = val;
        }


        public int executeSQL(String sql)
        {
            int ret = 0;
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Transaction = trn;
                ret = cmd.ExecuteNonQuery();
            }
            return ret;
        }

        public int insert(String table)
        {
            string fields = "";
            string values = "";
            foreach (string name in vals.Keys)
            {
                if (fields != "")
                {
                    fields += ", ";
                    values += ", ";
                }
                fields += "\"" + name + "\"";
                values += "@" + name;
            }

            if (extraFields != "")
                fields += ", " + extraFields;

            if (extraValues != "")
                values += ", " + extraValues;


            String sql = String.Format("INSERT INTO \"{0}\" ({1}) VALUES ({2});", table, fields, values);

            int ret = 0;
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Transaction = trn;
                foreach (string name in vals.Keys)
                {
                    cmd.Parameters.AddWithValue("@" + name, vals[name]);
                }

                try
                {
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new Exception(sql);
                }
            }


            return ret;
        }

        public int edit(String table)
        {
            // format value statement
            string values = "";
            foreach (string name in vals.Keys)
            {
                if (values != "")
                    values += ", ";
                values += name + " = @" + name;
            }

            // format where statement
            string where = "";
            foreach (string name in wheres.Keys)
            {
                if (where != "")
                    where += " AND ";
                where += name + " = " + "@" + name;
            }

            String sql = String.Format("UPDATE {0} SET {1} WHERE {2}", table, values, where);

            int ret = 0;
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Transaction = trn;
                foreach (string name in vals.Keys)
                    cmd.Parameters.AddWithValue("@" + name, vals[name]);
                foreach (string name in wheres.Keys)
                    cmd.Parameters.AddWithValue("@" + name, wheres[name]);
                ret = cmd.ExecuteNonQuery();
            }
            return ret;
        }

        public object select(String table, String field)
        {
            string values = "";
            foreach (string name in vals.Keys)
            {
                if (values != "")
                    values += " AND ";
                values += name + " = " + "@" + name;
            }

            String sql = String.Format("SELECT {0} FROM {1} WHERE {2}", field, table, values);

            object ret = null;
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Transaction = trn;
                foreach (string name in vals.Keys)
                    cmd.Parameters.AddWithValue("@" + name, vals[name]);

                using (SqlDataReader set = cmd.ExecuteReader())
                {
                    if (set.Read())
                        ret = set[field];
                }
            }
            return ret;
        }

        public void delete(String table)
        {
            string values = "";
            foreach (string name in vals.Keys)
            {
                if (values != "")
                    values += " AND ";
                values += name + " = " + "@" + name;
            }

            String sql = String.Format("DELETE FROM {0} WHERE {1}", table, values);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Transaction = trn;
                foreach (string name in vals.Keys)
                    cmd.Parameters.AddWithValue("@" + name, vals[name]);
                cmd.ExecuteNonQuery();
            }

        }

        public int selectInt(String table, String field)
        {
            object ret = select(table, field);
            return ret != null ? (int)ret : -1;
        }

        public string selectString(String table, String field)
        {
            object ret = select(table, field);
            return ret != null ? (string)ret : "";
        }

        public void clear()
        {
            vals.Clear();
            wheres.Clear();
        }


        public string extraFields = "";
        public string extraValues = "";



        protected SqlConnection con;
        protected SqlTransaction trn;
        protected HttpResponse rsp;
        protected Dictionary<string, object> vals = new Dictionary<string, object>();
        protected Dictionary<string, object> wheres = new Dictionary<string, object>();
    }
}