using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task.Models;

namespace Task.Repo
{
    public class Repository
    {
        private SqlConnection _connection;

        public Repository()
        {
            string connectionString = "Server=.;Database=Task;Trusted_Connection=Yes";

            _connection = new SqlConnection(connectionString);
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlCommand cmd = new SqlCommand("getEmployees", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow emp in dataTable.Rows)
            {
                employees.Add(new Employee
                {
                    Id = Convert.ToInt32(emp["ID"]),
                    Name = emp["EmployeeName"].ToString(),
                    Department = emp["EmployeeDept"].ToString(),
                    Salary = Convert.ToInt32(emp["EmployeeSalary"]),
                    State = emp["EmployeeState"].ToString(),
                    Talukha = emp["EmployeeTalukha"].ToString(),
                    City = emp["EmployeeCity"].ToString()
                });
            }

            return employees;
        }

        public List<States> GetStates()
        {
            List<States> states = new List<States>();
            SqlCommand cmd = new SqlCommand("GetStates", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow emp in dataTable.Rows)
            {
                states.Add(new States
                {
                    StateID = Convert.ToInt32(emp["StateID"]),
                    StateName = emp["StateName"].ToString()
                });
            }

            return states;
        }

        public List<Talukha> GetTalukas(int stateId)
        {
            List<Talukha> talukhas = new List<Talukha>();

            SqlCommand command = new SqlCommand("GetTalukhasByState", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter("@StateID", stateId);
            command.Parameters.Add(parameter);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow talukhaRow in dataTable.Rows)
            {
                Talukha talukha = new Talukha
                {
                    TalukhaID = Convert.ToInt32(talukhaRow["TalukhaID"]),
                    TalukhaName = talukhaRow["TalukhaName"].ToString()
                };
                talukhas.Add(talukha);
            }

            return talukhas;
        }

        public List<Cities> GetCities(int talukhaId)
        {
            List<Cities> cities = new List<Cities>();

            SqlCommand command = new SqlCommand("GetCitiesByTalukha", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter("@TalukhaID", talukhaId);
            command.Parameters.Add(parameter);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow talukhaRow in dataTable.Rows)
            {
                Cities city = new Cities
                {
                    CityID = Convert.ToInt32(talukhaRow["CityID"]),
                    CityName = talukhaRow["CityName"].ToString()
                };
                cities.Add(city);
            }

            return cities;
        }

        public Employee GetEmployeesByID(int id)
        {
            Employee employees = new Employee();

            SqlCommand command = new SqlCommand("getEmployeeById", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter;
            command.Parameters.Add(new SqlParameter("@ID", id));

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow emp in dataTable.Rows)
            {
                employees = new Employee
                {
                    Id = Convert.ToInt32(emp["ID"]),
                    Name = emp["EmployeeName"].ToString(),
                    Department = emp["EmployeeDept"].ToString(),
                    Salary = Convert.ToInt32(emp["EmployeeSalary"]),
                    State = emp["EmployeeState"].ToString(),
                    Talukha = emp["EmployeeTalukha"].ToString(),
                    City = emp["EmployeeCity"].ToString()
                };
            }

            return employees;
        }

        public bool AddEmployee(Employee employee)
        {
            SqlCommand command = new SqlCommand("addEmployee", _connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeName", employee.Name);
            command.Parameters.AddWithValue("@EmployeeSalary", employee.Salary);
            command.Parameters.AddWithValue("@EmployeeDept", employee.Department);
            command.Parameters.AddWithValue("@EmployeeState", employee.State);
            command.Parameters.AddWithValue("@EmployeeTalukha", employee.Talukha);
            command.Parameters.AddWithValue("@EmployeeCity", employee.City);

            _connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            _connection.Close();

            if (rowAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEmployeeLocation(int employeeId, string stateName, string talukhaName, string cityName)
        {
            SqlCommand cmd = new SqlCommand("UpdateEmployeeLocation", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            cmd.Parameters.AddWithValue("@StateName", stateName);
            cmd.Parameters.AddWithValue("@TalukhaName", talukhaName);
            cmd.Parameters.AddWithValue("@CityName", cityName);

            _connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            _connection.Close();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrder(int orderId, DateTime orderDate, int orderQuantity, int sales, string shipMode, int profit, int unitPrice, string customerName, string customerSegment, string productCategory)
        {
            using (SqlCommand cmd = new SqlCommand("InsertOrder", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                cmd.Parameters.AddWithValue("@OrderQuantity", orderQuantity);
                cmd.Parameters.AddWithValue("@Sales", sales);
                cmd.Parameters.AddWithValue("@ShipMode", shipMode);
                cmd.Parameters.AddWithValue("@Profit", profit);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@CustomerSegment", customerSegment);
                cmd.Parameters.AddWithValue("@ProductCategory", productCategory);

                _connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                _connection.Close();

                return rowsAffected > 0;
            }
        }

        public List<Orders> GetOrders()
        {
            List<Orders> orders = new List<Orders>();

            SqlCommand cmd = new SqlCommand("GetOrder", _connection);

            cmd.CommandType = CommandType.StoredProcedure;

            _connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Orders order = new Orders
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]),
                        Sales = Convert.ToInt32(reader["Sales"]),
                        ShipMode = reader["ShipMode"].ToString(),
                        Profit = Convert.ToInt32(reader["Profit"]),
                        UnitPrice = Convert.ToInt32(reader["UnitPrice"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerSegment = reader["CustomerSegment"].ToString(),
                        ProductCategory = reader["ProductCategory"].ToString()
                    };

                    orders.Add(order);
                }
            }
            _connection.Close();

            return orders;
        }
    }
}

//OrderID   OrderDate OrderQuantity	Sales	ShipMode	Profit	UnitPrice	CustomerName	CustomerSegment	ProductCategory