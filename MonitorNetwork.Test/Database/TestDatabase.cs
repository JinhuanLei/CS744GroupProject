using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MonitorNetwork.Test
{
    [TestClass]
    public class TestDatabase
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

        [TestMethod]
        public void TestConnection()
        {

            MySqlConnection cnn = new MySqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                cnn.Ping();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [TestMethod]
        public void TestDataEntry()
        {
            string query = "INSERT INTO new_table (new_tablecol) VALUES('testvalue2')";
            MySqlConnection cnn = new MySqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
