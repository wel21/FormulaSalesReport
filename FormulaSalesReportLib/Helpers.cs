using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.ComponentModel;

namespace FormulaReportsLib
{
    static class Helpers
    {
        public static Image ConvBase64ToImage(string s)
        {
            
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(s);
            System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms1.Write(imageBytes, 0, imageBytes.Length);
            Image img1 = Image.FromStream(ms1, false);

            return img1;

        }

        public static string ConvertMyDate(DateTime date)
        {
            //string s = value;
            //string r = "";
            //if (s.Substring(4, 1) != "/")
            //{
            //    string sd = (Convert.ToInt32(s.Substring(0, s.IndexOf('/'))) < 10 ? "0" : "") + s.Substring(0, s.IndexOf('/'));
            //    string s1 = s.Substring(s.IndexOf('/') + 1, s.LastIndexOf('/') - s.IndexOf('/') - 1);
            //    string sm = (Convert.ToInt32(s1) < 10 ? "0" : "") + s1;
            //    r = s.Substring(s.Length - 4) + "/" + sd + "/" + sm;
            //}
            //else
            //    r = value;
            //_ParameterDate = r;
            return date.ToString("yyyy/MM/dd");
        }

        public static string NullToStr(object s, bool ReturnZero = false)
        {
            try
            {
                if (s != null)
                    return s.ToString();
                else
                {
                    if (ReturnZero)
                        return "0";
                    else
                        return "";
                }
            }
            catch
            {
                if (ReturnZero)
                    return "0";
                else
                    return "";
            }
        }

        public static float NullToFlt(object s)
        {
            try
            {
                if (s != null)
                    return (float)Convert.ToDouble(s);
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static string LookUpDataTable(DataTable LookIn, string FieldToLook, string Param)
        {
            DataRow[] drarray = null;
            drarray = LookIn.Select(Param);
            return NullToStr(drarray[0][FieldToLook]);
        }

        public static List<string> LookUpDataTable(DataTable LookIn, List<string> FieldToLook, string Param)
        {
            List<string> s = new List<string>();
            DataRow[] drarray = null;
            drarray = LookIn.Select(Param);
            for (int i = 0; i < FieldToLook.Count; i++)
            {
                if(drarray.Count() == 0)
                    s.Add("");
                else
                    s.Add(NullToStr(drarray[0][FieldToLook[i]]));
            }
            return s;
        }
        
        public enum eReturnTime
        {
            Hours,
            Minutes,
            Seconds
        }
        public static string GetDurationInterval(string startTime, string endTime, eReturnTime ReturnTime)
        {
            try
            {
                if (endTime == "  :" | string.IsNullOrEmpty(endTime))
                    return "";
                if (startTime == "  :" | string.IsNullOrEmpty(startTime))
                    return "";

                int iStart = Convert.ToInt32(startTime.Substring(0, startTime.IndexOf(':')));
                int iEnd = Convert.ToInt32(endTime.Substring(0, endTime.IndexOf(':')));

                if (iStart > iEnd)
                {
                    //if istart > iend then subtract istart with iend to compute the duration
                    //ie: 22:00 - 02:00 (10:00pm - 02:00am); to; 20:00 - 24:00
                    //the TimeSpan will read it as 02:00 - 22:00 and will return 20:00 instead of 04:00
                    startTime = (iStart - iEnd - 1).ToString("00") + startTime.Substring(2);
                    endTime = "23" + endTime.Substring(2);
                }

                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

                if (ReturnTime == eReturnTime.Hours)
                    return duration.TotalHours.ToString("0.##");
                else if (ReturnTime == eReturnTime.Minutes)
                    return duration.TotalMinutes.ToString("0.##");
                else
                    return duration.TotalSeconds.ToString();
                //return duration.ToString().Substring(0, 5);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        
    }

    public static class DatabaseConnectionSettings
    {
        public static string GetConnectionString()
        {
            string s = "";
            s = "Server=" + Server + ";" +
                "User Id=" + User + ";" +
                "password=" + Password + ";" +
                "Persist Security Info=True;" +
                "database=" + Database + "";
            return s;
        }

        public static string SetConnectionString
        {
            set
            {
                string s = value.ToLower();
                if (s.Substring(s.Length) != ";") s = s + ";";

                Server = connstruct1(s, "server=");
                User = connstruct1(s, "user id=");
                if (User == "") User = connstruct1(s, "userid");
                Password = connstruct1(s, "password=");
                Database = connstruct1(s, "database=");
            }
        }

        private static string _Server = "localhost";
        public static string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                _Server = value;
            }
        }

        private static string _User = "root";
        public static string User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
            }
        }

        private static string _Password = "root";
        public static string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        private static string _Database = "purepos";
        public static string Database
        {
            get
            {
                return _Database;
            }
            set
            {
                _Database = value;
            }
        }

        private static string _Port = "3306";
        public static string Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }

        private static string connstruct1(string s, string strcriteria)
        {
            try
            {
                return s.Substring(connstruct2(s, strcriteria), (s.Substring(connstruct2(s, strcriteria))).IndexOf(";"));
            }
            catch
            {
                return "";
            }
        }

        private static int connstruct2(string s, string strcriteria)
        {
            return s.IndexOf(strcriteria.ToLower()) + strcriteria.ToLower().Length;
        }

    }

    public class DatabaseConnection
    {
        public string MyConString;
        public MySqlConnection connection;
        public MySqlCommand dbCommand;

        public DatabaseConnection()
        {
            dbCommand = new MySqlCommand();
        }

        public MySql.Data.MySqlClient.MySqlConnection connectToDB()
        {
            MyConString = DatabaseConnectionSettings.GetConnectionString();
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = MyConString;
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return connection;
        }
    }
    
    public class CReportData : DatabaseConnection
    {
        public MySqlCommand command;
        public MySqlDataReader Reader;
        
        public DataTable ProcessReportData(string query, List<string> paramfields, List<string> paramvalues)
        {
            DatabaseConnection con = new DatabaseConnection();
            connection = con.connectToDB();
            string commandString;
            //commandString = "SELECT * FROM employeejobs" + (param == "" ? "" : " WHERE " + param);

            commandString = query;

            command = connection.CreateCommand();
            command.CommandText = commandString;
            //command.Parameters.Add(new MySqlParameter("employee", employee));
            if (paramfields != null)
            {
                for (int i = 0; i < paramfields.Count(); i++)
                {
                    command.Parameters.Add(new MySqlParameter(paramfields[i].Replace("@", ""), paramvalues[i]));
                }
            }
            MySqlDataReader Reader = command.ExecuteReader();
            DataTable dt = new DataTable();

            if (Reader.HasRows)
            {
                dt.Load(Reader);
            }
            else
            {
                dt = null;                
            }

            connection.Close();
            return dt;
        }
    }

    public class CRStoreData : DatabaseConnection
    {
        public MySqlCommand command;
        public MySqlDataReader Reader;
        public StoreInfo StoreInformation = new StoreInfo();
        
        public void StoreInfoPopulate()
        {
            DatabaseConnection con = new DatabaseConnection();
            connection = con.connectToDB();
            string commandString;
            //commandString = "SELECT * FROM employeejobs" + (param == "" ? "" : " WHERE " + param);

            commandString = "SELECT * FROM storeinformation";

            command = connection.CreateCommand();
            command.CommandText = commandString;
            //command.Parameters.Add(new MySqlParameter("employee", employee));
            //for (int i = 0; i < paramfields.Count(); i++)
            //{
            //    command.Parameters.Add(new MySqlParameter(paramfields[i].Replace("@", ""), paramvalues[i]));
            //}
            MySqlDataReader Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                StoreInformation.StoreName = Reader["Name"].ToString();
                StoreInformation.StoreAddress = Reader["Address1"].ToString() + " " + Reader["Address2"].ToString();
                StoreInformation.StoreNumber = Reader["PhoneNumber"].ToString();
            }

            connection.Close();
        }
    }

    #region Extensions

    public static class DecimalExtensions
    {

        public static string AddComma(this decimal d, int DecimalPlaces = 2)
        {
            string sDec = "";
            for (int i = 1; i <= DecimalPlaces; i++)
            {
                if (string.IsNullOrEmpty(sDec))
                    sDec = ".";
                sDec = sDec + "0";
            }
            if (d.ToString().Contains("."))
            {
                //decimal d1 = d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal();
                if (d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal() == 0)
                {
                    sDec = "";
                }
            }
            if (d.ToString().Contains("."))
            {
                string s = d.ToString("#,###,##0" + sDec);
                decimal tmpD = Convert.ToDecimal(s); //String.Format(d.ToString(), "#,###,##0" + sDec);
                if (Convert.ToDecimal(tmpD.ToString().Substring(tmpD.ToString().IndexOf(".") + 1)) == 0)
                    sDec = "";
            }
            else
            {
                sDec = "";
            }

            return d.ToString("#,###,##0" + sDec);
        }

        public static string RemoveComma(this decimal d)
        {
            return d.ToString().Replace(",", "");
        }

        public static string DecimalPlace(this decimal d, int DecimalPlaces = 2, bool ShowZero = false)
        {
            string sDec = "";
            for (int i = 1; i <= DecimalPlaces; i++)
            {
                if (string.IsNullOrEmpty(sDec))
                    sDec = ".";
                sDec = sDec + "0";
            }
            if (d.ToString().Contains("."))
            {
                if (d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal() == 0)
                {
                    sDec = "";
                }
            }
            if (d.ToString().Contains("."))
            {
                string s = d.ToString((ShowZero ? "0" : "#") + sDec);
                decimal tmpD = Convert.ToDecimal(s);
                if (Convert.ToDecimal(tmpD.ToString().Substring(tmpD.ToString().IndexOf(".") + 1)) == 0)
                    sDec = "";
            }
            else
            {
                sDec = "";
            }

            return d.ToString((ShowZero ? "0" : "#") + sDec);
        }

    }

    public static class FloatExtensions
    {
        public static string AddComma(this float d, int DecimalPlaces = 2)
        {
            string sDec = "";
            for (int i = 1; i <= DecimalPlaces; i++)
            {
                if (string.IsNullOrEmpty(sDec))
                    sDec = ".";
                sDec = sDec + "0";
            }
            if (d.ToString().Contains("."))
            {
                //decimal d1 = d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal();
                if (d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal() == 0)
                {
                    sDec = "";
                }
            }
            if (d.ToString().Contains("."))
            {
                string s = d.ToString("#,###,##0" + sDec);
                float tmpD = (float)Convert.ToDecimal(s); //String.Format(d.ToString(), "#,###,##0" + sDec);
                if (Convert.ToDecimal(tmpD.ToString().Substring(tmpD.ToString().IndexOf(".") + 1)) == 0)
                    sDec = "";
            }
            else
            {
                sDec = "";
            }

            return d.ToString("#,###,##0" + sDec);
        }

        public static string RemoveComma(this float d)
        {
            return d.ToString().Replace(",", "");
        }

        public static string DecimalPlace(this float d, int DecimalPlaces = 2)
        {
            string sDec = "";
            for (int i = 1; i <= DecimalPlaces; i++)
            {
                if (string.IsNullOrEmpty(sDec))
                    sDec = ".";
                sDec = sDec + "0";
            }
            if (d.ToString().Contains("."))
            {
                if (d.ToString().Substring(d.ToString().IndexOf(".") + 1).ToDecimal() == 0)
                {
                    sDec = "";
                }
            }
            if (d.ToString().Contains("."))
            {
                string s = d.ToString("#" + sDec);
                float tmpD = (float)Convert.ToDecimal(s);
                if (Convert.ToDecimal(tmpD.ToString().Substring(tmpD.ToString().IndexOf(".") + 1)) == 0)
                    sDec = "";
            }
            else
            {
                sDec = "";
            }

            return d.ToString("#" + sDec);// String.Format(d.ToString(), "0" + sDec);
        }

    }

    public static class StringExtensions
    {
        public static string RemoveComma(this string s)
        {
            s = s.Replace("%", "");
            return s.Replace(",", "");
        }

        public static decimal ToDecimal(this string s)
        {
            if (s == null)
                s = "0";
            if (string.IsNullOrEmpty(s))
                s = "0";
            return Convert.ToDecimal(s);
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }

    public static class IntegerExtensions
    {

        public static string AddComma(this int d)
        {
            return d.ToString("#,###,##0");
        }

    }

    #endregion

    #region Comparer

    public class ReportDataComparer : IComparer<ReportData>
    {
        public int Compare(ReportData x, ReportData y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int retval = x.Data.CompareTo(y.Data);// & x.Data1.CompareTo (y.Data1);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }

    #endregion

}
