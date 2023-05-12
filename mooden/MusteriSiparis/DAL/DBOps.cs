using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    ///<summary>
    ///Ilerleyen Teknoloji - itToyz v0.01 - Database Fonksiyonlari - 2020.05.02
    ///</summary>
    public class DBOps
    {
        private string _ConnStr = "";

        public DBOps(string ConnStr)
        {
            _ConnStr = (ConnStr.ToLower().Length <= 20 || ConnStr.ToLower().StartsWith("connstr") ? ConfigurationManager.ConnectionStrings[ConnStr].ConnectionString : ConnStr);
            if (ConnStr.ToLower().Contains("integrated security") || ConnStr.ToLower().Contains("data source") ||
            ConnStr.ToLower().Contains("persist security info") || ConnStr.ToLower().Contains("initial catalog"))
                _ConnStr = ConnStr;
        }
        public DataTable sc(string Cumle, params object[] Pler)
        {
            string cmdlog = "";
            DataTable dt = new DataTable("sc");
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    using (SqlDataAdapter adap = new SqlDataAdapter(Cumle, conn))
                    {
                        conn.Open();
                        adap.SelectCommand.Parameters.Clear();
                        if (Pler != null)
                            for (int i = 0; i < Pler.Length; i++)
                            {
                                //Type t = Pler[i].GetType();if (t.Equals(typeof(int))) { comm.Parameters.AddWithValue("@P" + (i + 1).ToString(), (int)Pler[i]); }else
                                if (adap.SelectCommand.CommandText.ToLower().Contains("@p" + (i + 1).ToString()))
                                    adap.SelectCommand.Parameters.AddWithValue("@P" + (i + 1).ToString(),
                                         (Pler[i] != null ? Pler[i] : DBNull.Value));
                            }
                        cmdlog = getGeneratedSql(adap.SelectCommand);
                        adap.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                //new Logs().Logla("", "", "sc-tryc", Cumle + " : " + cmdlog, "dbops");
                return dt;
            }
            return dt;
        }

        ///<summary>
        ///Scalar sonucu String olarak doner. Hata durumunda '-' doner.
        ///</summary>
        public string scs(string Cumle, params object[] Pler)
        {
            string cmdlog = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand comm = new SqlCommand(Cumle, conn))
                    {
                        conn.Open();
                        comm.Parameters.Clear();
                        //if (_trans != null) { _trans = conn.BeginTransaction(); }
                        if (Pler != null)
                            for (int i = 0; i < Pler.Length; i++)
                            {
                                //Type t = Pler[i].GetType();if (t.Equals(typeof(int))) { comm.Parameters.AddWithValue("@P" + (i + 1).ToString(), (int)Pler[i]); }else
                                if (Cumle.ToLower().Contains("@p" + (i + 1).ToString()))
                                    comm.Parameters.AddWithValue("@P" + (i + 1).ToString(),
                                            (Pler[i] != null ? Pler[i] : DBNull.Value));
                            }
                        cmdlog = getGeneratedSql(comm);
                        var donen = comm.ExecuteScalar();
                        return (donen == null ? null : donen.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //new Logs().Logla("", "", "scs-tryc", Cumle + " : " + cmdlog, "dbops");
                return "-";
            }
        }

        ///<summary>
        ///Etkilenen satir sayisi doner. Hata durumunda -1 doner.
        ///</summary>
        public int scn(string Cumle, params object[] Pler)
        {
            string cmdlog = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand comm = new SqlCommand(Cumle, conn))
                    {
                        conn.Open();
                        comm.Parameters.Clear();
                        if (Pler != null)
                            for (int i = 0; i < Pler.Length; i++)
                            {
                                //Type t = Pler[i].GetType();if (t.Equals(typeof(int))) { comm.Parameters.AddWithValue("@P" + (i + 1).ToString(), (int)Pler[i]); }else
                                if (Cumle.ToLower().Contains("@p" + (i + 1).ToString()))
                                    comm.Parameters.AddWithValue("@P" + (i + 1).ToString(),
                                            (Pler[i] != null ? Pler[i] : DBNull.Value));
                            }
                        cmdlog = getGeneratedSql(comm);
                        int sonuc = comm.ExecuteNonQuery();
                        return sonuc;
                    }
                }
            }
            catch (Exception ex)
            {
                /*new Logs().Logla("", "", "scn-tryc", Cumle + " : " + cmdlog, "dbops");*/
                return -1;
            }
        }

        ///<summary>
        ///Params tablosundan PValue degerini doner. 
        ///</summary>
        ///<param name="PName">PName degeri</param>
        public string ParamsVer(string PName)
        {
            //bunu
            string cmdlog = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand comm = new SqlCommand("select top 1 PVALUE from PArams where PNAme=@P1", conn))
                    {
                        conn.Open();
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@P1", PName);
                        cmdlog = getGeneratedSql(comm);
                        return comm.ExecuteScalar().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //new Logs().Logla("", "", "ParamsVer-tryc", PName + " : " + cmdlog, "dbops");
                return "";
            }
            return cmdlog;
        }

        private string getGeneratedSql(SqlCommand cmd)
        {
            string result = cmd.CommandText.ToString();
            foreach (SqlParameter p in cmd.Parameters)
            {
                try
                {
                    string isQuted = (p.Value is string) ? "'" : "";
                    result = result.Replace('@' + p.ParameterName.ToString(), isQuted + p.Value.ToString() + isQuted);
                }
                catch (Exception)
                { }
            }
            return result;
        }

        public static string tmz(string g)
        {
            if (g != null)
            {
                return g.Replace("'", "").Replace("\"", "").Replace("DROP", "").Replace("SELECT", "").Replace("UPDATE", "").Replace("DELETE", "").Replace("INSERT", "").Replace("TRUNCATE", "").Replace("TABLE", "");
            }
            return g;
        }

        ///<summary>
        ///Etkilenen satir sayisi doner. Hata durumunda -1 doner.
        ///</summary>
        public object scex(string Cumle, params object[] Pler)
        {
            string cmdlog = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand comm = new SqlCommand(Cumle, conn))
                    {
                        conn.Open();
                        comm.Parameters.Clear();
                        if (Pler != null)
                            for (int i = 0; i < Pler.Length; i++)
                            {
                                //Type t = Pler[i].GetType();if (t.Equals(typeof(int))) { comm.Parameters.AddWithValue("@P" + (i + 1).ToString(), (int)Pler[i]); }else
                                if (Cumle.ToLower().Contains("@p" + (i + 1).ToString()))
                                    comm.Parameters.AddWithValue("@P" + (i + 1).ToString(),
                                            (Pler[i] != null ? Pler[i] : DBNull.Value));
                            }
                        cmdlog = getGeneratedSql(comm);
                        var sonuc = comm.ExecuteScalar();
                        return sonuc;
                    }
                }
            }
            catch (Exception ex)
            {
                //new Logs().Logla("", "", "scn-tryc", Cumle + " : " + cmdlog, "dbops");
                return -1;
            }
        }

    }
}