﻿using appSql.Models;
using System.Data.SqlClient;

namespace appSql.Services
{
    public class CourseSerivce
    {
        // Ensure to change the below variables to reflect the connection details for your database
        private static string db_source = "dbshashi.database.windows.net";
        private static string db_user = "dbAdmin";
        private static string db_password = "March_2022jan";
        private static string db_database = "AppDbShashi";

        private SqlConnection GetConnection(string _connection_string)
        {
            // Here we are creating the SQL connection
            //var _builder = new SqlConnectionStringBuilder();
            //_builder.DataSource = db_source;
            //_builder.UserID = db_user;
            //_builder.Password = db_password;
            //_builder.InitialCatalog = db_database;
            return new SqlConnection(_connection_string);
        }

        public IEnumerable<Course> GetCourses(string _connection_string)
        {
            List<Course> _lst = new List<Course>();
            string _statement = "SELECT CourseID,CourseName,rating from Course";
            SqlConnection _connection = GetConnection(_connection_string);
            // Let's open the connection
            _connection.Open();
            // We then construct the statement of getting the data from the Course table
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            // Using the SqlDataReader class , we will read all the data from the Course table
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Course _course = new Course()
                    {
                        CourseID = _reader.GetInt32(0),
                        CourseName = _reader.GetString(1),
                        Rating = _reader.GetDecimal(2)
                    };

                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return _lst;
        }
    }
}
