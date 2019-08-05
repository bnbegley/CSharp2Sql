using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp2sqlProject {
    class Program {
        static void Main(string[] args) {
            var ord = GetOrderbyId(2002);
            if 
                (ord == null) {
                Console.WriteLine("Order not found.");

            }
            else {
                Console.WriteLine($"{ord.Id} | {ord.CustomerId} | {ord.Date}");
            }
            var cust = GetCustomerbyId(1024);
            if (cust == null) {
                Console.WriteLine("Customer not found.");
            }
            else {
                Console.WriteLine(cust.Name);
            }

            var sql = " SELECT * from Customers where id = '1015';";
            var Orders = SelectOrder("Select * from Orders");
            foreach (var order in Orders) {
                Console.WriteLine($"{order.Id} | {order.CustomerId} | {order.Date} ");
            }


            var customers = SelectCustomer(sql);
            foreach (var customer in customers) {
                Console.WriteLine(customer.Name);
            }

        }
        static List<Order> SelectOrder(string sql) {
            var connectionstring = @"server=localhost\sqlexpress;
                database=CustomerOrderDb;
                trusted_connection=true;";

            var connection = new SqlConnection(connectionstring);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var orderlist = new List<Order>();
            var comm = new SqlCommand(sql, connection);
            var reader = comm.ExecuteReader();
            while (reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note"))
                    ? null
                    : reader["Note"].ToString();
                var customerid = -1;
                if (reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    customerid = 0;
                } else {
                    customerid = (int)reader["CustomerId"];
                }

                var order = new Order(id, date, note, customerid);
                orderlist.Add(order);


            }
            reader.Close();
            connection.Close();
            return orderlist;
        }

        static List<Customer> SelectCustomer(string sql) {
            var connStr =
                @"server=localhost\sqlexpress;
                database=CustomerOrderDb;
                trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
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
        static Customer GetCustomerbyId(int pid) {
            var connStr =
               @"server=localhost\sqlexpress;
                database=CustomerOrderDb;
                trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var sql = "SELECT * From Customers where Id = @myid;";
            var cmd = new SqlCommand(sql, connection);
            var theId = new SqlParameter("@myid", pid);
            cmd.Parameters.Add(theId);
            var reader = cmd.ExecuteReader();
            Customer cust = null;
            if (reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                    ? null
                    : reader["Code"].ToString();
                cust = new Customer(id, name, city, state, active, code);
            }


            reader.Close();
            connection.Close();
            return cust;




        }

        static Order GetOrderbyId(int pid)
        {
        var connStr =
              @"server=localhost\sqlexpress;
                database=CustomerOrderDb;
                trusted_connection=true;";
        var connection = new SqlConnection(connStr);
        connection.Open();
            if (connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!"); 
                } var sql = "Select * from Orders where Id = @myid;";
            var cmd = new SqlCommand(sql, connection);
            var orderid = new SqlParameter("@myid", pid);
            cmd.Parameters.Add(orderid);
            var reader = cmd.ExecuteReader();
            Order ord = null;
            if (reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note"))
                    ? null
                    : reader["Note"].ToString();
                var customerid = -1;
                if (reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    customerid = 0;
                }
                else {
                    customerid = (int)reader["CustomerId"];
                }
                ord = new Order(id, date, note, customerid);
             }
            reader.Close();
            connection.Close();

            return ord;
           }
         }
       }
     
