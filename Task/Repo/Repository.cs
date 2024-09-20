using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task.Models;

namespace Task.Repo
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository()
        {
            _connectionString = "Server=DESKTOP-U9TQCSK\\SQLEXPRESS01;Database=Task;Trusted_Connection=Yes";
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("getEmployees", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Name = reader["EmployeeName"].ToString(),
                                Department = reader["EmployeeDept"].ToString(),
                                Salary = Convert.ToInt32(reader["EmployeeSalary"]),
                                State = reader["EmployeeState"].ToString(),
                                Talukha = reader["EmployeeTalukha"].ToString(),
                                City = reader["EmployeeCity"].ToString()
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public List<States> GetStates()
        {
            List<States> states = new List<States>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetStates", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new States
                            {
                                StateID = Convert.ToInt32(reader["StateID"]),
                                StateName = reader["StateName"].ToString()
                            });
                        }
                    }
                }
            }
            return states;
        }

        public List<Talukha> GetTalukas(int stateId)
        {
            List<Talukha> talukhas = new List<Talukha>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetTalukhasByState", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateID", stateId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            talukhas.Add(new Talukha
                            {
                                TalukhaID = Convert.ToInt32(reader["TalukhaID"]),
                                TalukhaName = reader["TalukhaName"].ToString()
                            });
                        }
                    }
                }
            }
            return talukhas;
        }

        public List<Cities> GetCities(int talukhaId)
        {
            List<Cities> cities = new List<Cities>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetCitiesByTalukha", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TalukhaID", talukhaId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new Cities
                            {
                                CityID = Convert.ToInt32(reader["CityID"]),
                                CityName = reader["CityName"].ToString()
                            });
                        }
                    }
                }
            }
            return cities;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("getEmployeeById", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Name = reader["EmployeeName"].ToString(),
                                Department = reader["EmployeeDept"].ToString(),
                                Salary = Convert.ToInt32(reader["EmployeeSalary"]),
                                State = reader["EmployeeState"].ToString(),
                                Talukha = reader["EmployeeTalukha"].ToString(),
                                City = reader["EmployeeCity"].ToString()
                            };
                        }
                    }
                }
            }
            return employee;
        }

        public bool AddEmployee(Employee employee)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("addEmployee", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                    cmd.Parameters.AddWithValue("@EmployeeSalary", employee.Salary);
                    cmd.Parameters.AddWithValue("@EmployeeDept", employee.Department);
                    cmd.Parameters.AddWithValue("@EmployeeState", employee.State);
                    cmd.Parameters.AddWithValue("@EmployeeTalukha", employee.Talukha);
                    cmd.Parameters.AddWithValue("@EmployeeCity", employee.City);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateEmployeeLocation(int employeeId, string stateName, string talukhaName, string cityName)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateEmployeeLocation", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@StateName", stateName);
                    cmd.Parameters.AddWithValue("@TalukhaName", talukhaName);
                    cmd.Parameters.AddWithValue("@CityName", cityName);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool InsertOrder(Orders order)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("InsertOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@OrderQuantity", order.OrderQuantity);
                    cmd.Parameters.AddWithValue("@Sales", order.Sales);
                    cmd.Parameters.AddWithValue("@ShipMode", order.ShipMode);
                    cmd.Parameters.AddWithValue("@Profit", order.Profit);
                    cmd.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                    cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerSegment", order.CustomerSegment);
                    cmd.Parameters.AddWithValue("@ProductCategory", order.ProductCategory);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Orders> GetOrders()
        {
            List<Orders> orders = new List<Orders>();
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Orders
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
                            });
                        }
                    }
                }
            }
            return orders;
        }
    }
}

//OrderID   OrderDate OrderQuantity	Sales	ShipMode	Profit	UnitPrice	CustomerName	CustomerSegment	ProductCategory