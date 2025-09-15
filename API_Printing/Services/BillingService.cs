using API_Printing.Reports;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace API_Printing.Services
{
    public class BillingService
    {
        private readonly string _connectionString;

      
        public BillingService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevConnection");
        }
        public string GetMaintenanceBillingResult(string storedProcedureName, string BillingMonth,string BillingYear,string Block,string Sector)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                       
                        //command.Parameters.AddWithValue("@UIDs", uids);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                               
                                return "Data retrieved successfully";
                            }
                            else
                            {
                                return null; 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                    throw new Exception("An error occurred while retrieving billing data.", ex);
                }
            }
        }








        public string GetElectricityBillingResult(string storedProcedureName, string project, string block, string sector, string billingMonth, string billingYear)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set command timeout (e.g., 120 seconds)
                        command.CommandTimeout = 120;

                        // Add stored procedure parameters
                        command.Parameters.AddWithValue("@Project", project ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Block", block ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Sector", sector ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BillingMonth", billingMonth ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BillingYear", billingYear ?? (object)DBNull.Value);

                        // Optional: log execution time
                        var stopwatch = Stopwatch.StartNew();

                        using (var reader = command.ExecuteReader())
                        {
                            stopwatch.Stop();
                            Console.WriteLine($"Query executed in {stopwatch.Elapsed.TotalSeconds} seconds.");

                            // You can optionally process the reader here
                            // while (reader.Read()) { ... }

                            return "Data retrieved successfully";
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("SQL error occurred while retrieving billing data.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving billing data.", ex);
                }
            }
        }









        public string GetMaintenanceBillingResult(string storedProcedureName, string BillingMonth, string BillingYear)
    {
        string result = "";
        try
        {
          
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BillingMonth", BillingMonth);
                    command.Parameters.AddWithValue("@BillingYear", BillingYear);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader.GetString(1); 
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error occurred: " + ex.Message);
        }

        return result;
    }




        public string GetElectricityBillingResult(string storedProcedureName, string BillingMonth, string BillingYear)
        {
            string result = "";
            try
            {
                
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Month", BillingMonth);
                        command.Parameters.AddWithValue("@Year", BillingYear);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occurred: " + ex.Message);
            }

            return result;
        }




        public string GetElectricityBillingResultByUID(string storedProcedureName, string UidList)
        {
            string result = "";
            try
            {
               
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@Uid", UidList); 

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader.GetString(1); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occurred: " + ex.Message);
            }

            return result;
        }




        public string ServiceEBillByMonth(string BillingMonth, string BillingYear)
        {
            
            StringBuilder result = new StringBuilder();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetEBillByMonth", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Month", BillingMonth);
                        command.Parameters.AddWithValue("@Year", BillingYear);
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                        
                            result.AppendLine("UID | InvoiceNo | CustomerNo | CustomerName | BTNo | BillingMonth | BillingYear | BillAmount");

                            while (reader.Read())
                            {
                                result.AppendLine(
                                    $"{reader["uid"]} | {reader["InvoiceNo"]} | {reader["CustomerNo"]} | " +
                                    $"{reader["CustomerName"]} | {reader["BTNo"]} | {reader["BillingMonth"]} | " +
                                    $"{reader["BillingYear"]} | {reader["BillAmountInDueDate"]}"
                                );
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }
            return result.ToString();
        }






       public string ServiceEBillByUid(string uid)
        {
            
            StringBuilder result = new StringBuilder();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetEBillByUid", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Uid", uid);
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           

                            while (reader.Read())
                            {
                                result.AppendLine(
                                    $"{reader["uid"]} | {reader["InvoiceNo"]} | {reader["CustomerNo"]} | " +
                                    $"{reader["CustomerName"]} | {reader["BTNo"]} | {reader["BillingMonth"]} | " +
                                    $"{reader["BillingYear"]} | {reader["BillAmountInDueDate"]}"
                                );
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {

            }

            return result.ToString();
        }






        public string ServiceEBillByUids(string[] uids)
        {
           
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetEBillByUids", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Uids", string.Join(",", uids)); 

                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                result = reader.GetString(1); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return result.ToString();
        }






        //public string ServiceEBillByUids2(string storedProcedureName, string BillingMonth, string BillingYear, string uid)
        //{
        //    string result = "";
        //    try
        //    {
        //        // Connection string for SQL Server
        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand("GetEBillByUids", con))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@Uids", string.Join(",", uids)); // Pass multiple UIDs as comma-separated
        //                //command.Parameters.AddWithValue("@Uids", "1216,1217"); //
        //                con.Open();
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())  // 🔹 Loop through all results
        //                    {
        //                        reader.Read();
        //                        result = reader.GetString(1); // Adjust index or use column name if needed
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Error occurred: " + ex.Message);
        //    }

        //    return bills;  // Return a list of bills instead of a single string
        //}




    }
}

