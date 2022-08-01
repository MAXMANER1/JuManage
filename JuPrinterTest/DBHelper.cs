using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MySqlConnector;
namespace JuPrinterTest
{
    public class DBHelper
    {
        string connectionString = null;
        public DBHelper()
        {
            connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MariaDB"].ConnectionString;
        }
        public DBHelper(string pConnectionString)
        {
            connectionString = pConnectionString;
        }

        public bool ConnectionTest()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine(connection.State.ToString());
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection Error Exception : " + e.Message.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public System.Data.DataTable DoQuery(string pQuery)
        {
            if (pQuery == "" || pQuery == null)
            {
                return null;
            }
              
            System.Data.DataTable dtQuery = new System.Data.DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    connection.Open();
                    Console.WriteLine(connection.State.ToString());
                    try
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(pQuery, connection);
                        adapter.Fill(dtQuery);
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(log() + "  Exception: " + e.ToString());
                        dtQuery = null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return dtQuery;

                }
            }
        }

        private static void CreateFile(byte[] fileBuffer, string newFilePath)
        {
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(fileBuffer, 0, fileBuffer.Length);
            bw.Close();
            fs.Close();
        }
        public bool DoTransaction(string pQuery, string id)
        {
            bool result = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(pQuery, connection))
                {
                    connection.Open();
                    Console.WriteLine(connection.State.ToString());
                    cmd.CommandText = pQuery;
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = id;
                    //cmd.Parameters.Add("@face_template", MySqlDbType.Blob).Value = pImg;
                    string i = cmd.CommandText;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine(e.ToString());
                        result = false;
                    }
                    finally
                    {
                        Console.WriteLine("Transaction Result: " + result);
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
            return result;
        }
        private string log()
        {
            return "Method= " + System.Reflection.MethodBase.GetCurrentMethod().Name;
        }

    }
}