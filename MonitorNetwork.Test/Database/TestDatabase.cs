using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;

namespace MonitorNetwork.Test
{
    [TestClass]
    public class TestDatabase
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["MNDatabase"].ConnectionString;

        [TestMethod]
        public void TestConnection()
        {

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [TestMethod]
        public void TestInsertAccount()
        {
            string query = "INSERT INTO account (accountFirstName,accountLastName,address,phoneNumber,spendingLimit,balance) VALUES('Jane','Zhou','1234 La St.','6084334881','3000','5000')";

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
        public void TestInsertCreditCard()
        {
            string query = "INSERT INTO creditcard(cardNumber,expirationDate,securityCode,customerFirstName,customerLastName,accountID) VALUES('95001255923431','2017-12-20 12:30:30','125','Jane','Doe','2')";

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
        public void TestInsertRelay()
        {
            string query = "INSERT INTO relay(relayIP,status,isProcessingCenter) VALUES('192.168.1.1','1','1')";

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
        public void TestInsertRelay2()
        {
            string query = "INSERT INTO relay(relayIP,status,isProcessingCenter) VALUES('192.168.1.1','1','1')";

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [TestMethod]
        public void TestDeleteAccount()
        {
            string query = "DELETE FROM account WHERE accountID= 7 ";
            //INSERT INTO account (accountFirstName,accountLastName,address,phoneNumber,spendingLimit,balance) VALUES('Jane','Zhou','1234 La St.','6084334881','3000','5000')

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [TestMethod]
        public void TestUpdateAccount()
        {
            string query = "Update account set balance=10000 where accountid=8";
            //INSERT INTO account (accountFirstName,accountLastName,address,phoneNumber,spendingLimit,balance) VALUES('Jane','Zhou','1234 La St.','6084334881','3000','5000')

            SqlConnection cnn = new SqlConnection(myConnectionString);

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);

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

