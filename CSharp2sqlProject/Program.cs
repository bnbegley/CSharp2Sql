using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp2sqlProject {
    class Program {
        static void Main(string[] args) {
            var sql = " SELECT * from Customers where id = '1015';";
            var customers = SelectCustomer(sql);
            foreach (var customer in customers) {
                Console.WriteLine(customer.Name);
            }
        }
        static List<Customer> SelectCustomer(string sql) {
            var connStr =
                @"server=localhost\sqlexpress;
                database=CustomerOrderDb;
                trusted_connection=true;"; 
            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var customerList = new List<Customer>();
            var cmd = new SqlCommand(sql, connection);
           var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                    ? null
                    : reader["Code"].ToString();

                var customer = new Customer(id, name, city, state, active, code);
                customerList.Add(customer);    
            }
            
            reader.Close();
            connection.Close();
            return customerList;
        }
    }
   }
