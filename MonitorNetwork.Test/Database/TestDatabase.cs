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
            string query = "INSERT INTO relay(relayIP,status,isProcessingCenter) VALUES('192.168.1.2','1','1')";

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
        public void TestInsertRelayConnectionWeight()
        {
            string query = "INSERT INTO relayConnectionWeightID(weight,relay1,relay2) VALUES('5','1','2')";

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
        public void TestInsertStore()
        {
            string query = "INSERT INTO store(storeIP,merchantName) VALUES('42.13.145.135','Kwik Trip')";

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
        public void TestInsertStoreToRelay()
        {
            string query = "INSERT INTO storetorelay(storeID,relayID) VALUES('1','2')";

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
        public void TestInsertTransaction()
        {
            string query = "INSERT INTO transaction(timeOfTransaction,timeOfResponse,amount,inCredit,status,storeID,accountID) VALUES('2018-01-28 12:30:28','2018-01-28 12:30:28','20','1','1','1','1')";

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
        public void TestInsertUser()
        {
            string query = "INSERT INTO user(username,password,security1,answer1,security2,answer2,security3,answer3) VALUES('jdoe','jdoe123','Father's name?','Max Doe','Mother's name?','Smile Doe','GF's name','Cindy')";

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
            string query = "Update account set balance=10000 where accountID=8";
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
        public void TestUpdateCreditcard()
        {
            string query = "Update creditcard set accID=2 where cardID=8";
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
        public void TestUpdateRelay()
        {
            string query = "Update relay set isProcessingCenter=1 where relayID=1";
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
        public void TestUpdateRelayconnectionweight()
        {
            string query = "Update relayconnectionweight set relay2=3 where relayconnectionweightID=1";
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
        public void TestUpdateStore()
        {
            string query = "Update store set merchantName='Toppers' where storeID=1";
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
        public void TestUpdateStoretorelay()
        {
            string query = "Update storetorelay set relayID='3' where storeID=1";
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
        public void TestUpdateTransaction()
        {
            string query = "Update transaction set amount=2000 where transactionID=1000000001";
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
        public void TestUpdateUser()
        {
            string query = "Update user set username=jnathan1 where userID=1";
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
        public void TestDeleteUser()
        {
            string query = "DELETE FROM user WHERE userID= 1 ";
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
        public void TestDeleteTransaction()
        {
            string query = "DELETE FROM transaction WHERE transactionID= 1 ";
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
        public void TestDeleteStoretorelay()
        {
            string query = "DELETE FROM storetorelay WHERE storeID= 1 ";
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
        public void TestDeleteStore()
        {
            string query = "DELETE FROM store WHERE storeID= 1 ";
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
        public void TestDeleteRelayconnectionweight()
        {
            string query = "DELETE FROM relayconnectionweight WHERE relayconnectionweightID= 1 ";
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
        public void TestDeleterelay()
        {
            string query = "DELETE FROM relay WHERE relayID= 1 ";
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
         public void TestDeletecreditcard()
        {
            string query = "DELETE FROM creditcard WHERE creditcardID= 1 ";
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

