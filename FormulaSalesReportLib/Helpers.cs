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

namespace FormulaSalesReportLib
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

        public static T ToClass<T>(this IDictionary<string, string> source) where T : class, new()
        {
            Type type = typeof(T);
            T ret = new T();

            foreach (var keyValue in source)
            {
                var propertyInfo = type.GetProperty(keyValue.Key);
                propertyInfo.SetValue(ret, keyValue.Value.ToString().TestParse(propertyInfo.PropertyType), null);
            }

            return ret;
        }

        public static object TestParse(this string value, Type type)
        {
            return TypeDescriptor.GetConverter(type).ConvertFromString(value);
        }
    }

    public static class SalesReportDatabaseConnectionSettings
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
            MyConString = SalesReportDatabaseConnectionSettings.GetConnectionString();
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
            for (int i = 0; i < paramfields.Count(); i++)
            {
                command.Parameters.Add(new MySqlParameter(paramfields[i].Replace("@", ""), paramvalues[i]));
            }
            MySqlDataReader Reader = command.ExecuteReader();
            DataTable dt = new DataTable();

            if (Reader.HasRows)
            {
                dt.Load(Reader);
                return dt;
            }
            else
            {
                return null;                
            }
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
        }
    }

}
